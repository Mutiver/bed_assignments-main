using Hearthstone_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hearthstone_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IMongoCollection<Class> _classCollection;
        private readonly ILogger<ClassesController> _logger;

        public ClassesController(IOptions<MongoDbSettings> mongoDbSettings, ILogger<ClassesController> logger)
        {
            MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);

            _classCollection = database.GetCollection<Class>(mongoDbSettings.Value.ClassCollectionName);
            _logger = logger;
        }

        // GET: api/Classes
        [HttpGet]
        public ActionResult<List<Class>> Get()
        {
            _logger.LogInformation("GET api/classes called");
            return _classCollection.Find(classItem => true).ToList();
        }
    }
}