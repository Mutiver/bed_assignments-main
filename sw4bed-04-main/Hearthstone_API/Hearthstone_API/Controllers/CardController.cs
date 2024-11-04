using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Linq;
using System.Collections.Generic;
using Hearthstone_API.Models;
using Microsoft.Extensions.Options;
using Hearthstone_API.Services;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Hearthstone_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly IMongoCollection<Card> _cardCollection;
        private IMongoCollection<Class> _classCollection;
        private IMongoCollection<Set> _setCollection;
        private IMongoCollection<CardType> _cardTypeCollection;
        private IMongoCollection<Rarity> _rarityCollection;

		private readonly ILogger<CardsController> _logger;
		private readonly CardService _cardService;

		public CardsController(IOptions<MongoDbSettings> mongoDbSettings, ILogger<CardsController> logger, CardService cardService)
        {
            MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);

            _cardCollection = database.GetCollection<Card>(mongoDbSettings.Value.CardCollectionName);
            _logger = logger;
            _cardService = cardService;


        }

        // GET: api/Cards
        [HttpGet]
        public ActionResult<List<CardsWithMetadataDto>> Get(int? page, int? setid, string? artist, int? classid, int? rarityid)
        {
            _logger.LogInformation("GET api/cards called");

            int pageSize = 100;
            int pageNumber = (page ?? 1) - 1;

            FilterDefinition<Card> filter = Builders<Card>.Filter.Empty;

            if (setid.HasValue)
                filter = filter & Builders<Card>.Filter.Eq(card => card.SetId, setid.Value);
            if (!string.IsNullOrEmpty(artist))
                filter = filter & Builders<Card>.Filter.Eq(card => card.Artist, artist);
            if (classid.HasValue)
                filter = filter & Builders<Card>.Filter.Eq(card => card.ClassId, classid.Value);
            if (rarityid.HasValue)
                filter = filter & Builders<Card>.Filter.Eq(card => card.RarityId, rarityid.Value);

            var cards = _cardCollection.Find(filter).Skip(pageNumber * pageSize).Limit(pageSize).ToList();

            var cardsWithMetadata = _cardService.JoinCardsWithMetaData(cards);

			return cardsWithMetadata;

		}

	}
}
