using Hearthstone_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hearthstone_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetsController : ControllerBase
    {
        private readonly IMongoCollection<Set> _setCollection;
        private readonly ILogger<SetsController> _logger;

        public SetsController(IOptions<MongoDbSettings> mongoDbSettings, ILogger<SetsController> logger)
        {
            MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);

            _setCollection = database.GetCollection<Set>(mongoDbSettings.Value.SetCollectionName);
            _logger = logger;
        }

        // GET: api/Sets
        [HttpGet]
        public ActionResult<List<Set>> Get()
        {
            _logger.LogInformation("GET api/sets called");
            return _setCollection.Find(set => true).ToList();
        }
    }
}