using Hearthstone_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hearthstone_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaritiesController : ControllerBase
    {
        private readonly IMongoCollection<Rarity> _rarityCollection;
        private readonly ILogger<RaritiesController> _logger;

        public RaritiesController(IOptions<MongoDbSettings> mongoDbSettings, ILogger<RaritiesController> logger)
        {
            MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);

            _rarityCollection = database.GetCollection<Rarity>(mongoDbSettings.Value.RarityCollectionName);
            _logger = logger;
        }

        // GET: api/Rarities
        [HttpGet]
        public ActionResult<List<Rarity>> Get()
        {
            _logger.LogInformation("GET api/rarities called");
            return _rarityCollection.Find(rarity => true).ToList();
        }
    }
}