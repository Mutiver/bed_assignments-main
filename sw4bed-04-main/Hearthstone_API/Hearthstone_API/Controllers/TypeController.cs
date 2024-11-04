using Hearthstone_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hearthstone_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly IMongoCollection<CardType> _typeCollection;
        private readonly ILogger<TypesController> _logger;

        public TypesController(IOptions<MongoDbSettings> mongoDbSettings, ILogger<TypesController> logger)
        {
            MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);

            _typeCollection = database.GetCollection<CardType>(mongoDbSettings.Value.CardTypeCollectionName);
            _logger = logger;
        }

        // GET: api/Types
        [HttpGet]
        public ActionResult<List<CardType>> Get()
        {
            _logger.LogInformation("GET api/types called");
            return _typeCollection.Find(type => true).ToList();
            
        }
    }
}