using Hearthstone_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hearthstone_API.Services
{
	public class CardService
	{
		private IMongoCollection<Card> _cardCollection;
		private IMongoCollection<Class> _classCollection;
		private IMongoCollection<Set> _setCollection;
		private IMongoCollection<CardType> _cardTypeCollection;
		private IMongoCollection<Rarity> _rarityCollection;
		public CardService(IOptions<MongoDbSettings> mongoDbSettings)
		{
			MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
			IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);

			_cardCollection = database.GetCollection<Card>(mongoDbSettings.Value.CardCollectionName);
			_cardTypeCollection = database.GetCollection<CardType>(mongoDbSettings.Value.CardTypeCollectionName);
			_classCollection = database.GetCollection<Class>(mongoDbSettings.Value.ClassCollectionName);
			_rarityCollection = database.GetCollection<Rarity>(mongoDbSettings.Value.RarityCollectionName);
			_setCollection = database.GetCollection<Set>(mongoDbSettings.Value.SetCollectionName);

		}


		public List<CardsWithMetadataDto> JoinCardsWithMetaData(List<Card> cards)
		{
			var result = new List<CardsWithMetadataDto>();

			foreach (var card in cards)
			{
				result.Add(new CardsWithMetadataDto
				{
					Id = card.Id,
					Name = card.Name,
					Class = (from c in _classCollection.AsQueryable()
						where c.Id == card.ClassId
						select c.Name).FirstOrDefault(),

					CardType = (from t in _cardTypeCollection.AsQueryable()
						where t.Id == card.TypeId
						select t.Name).FirstOrDefault(),

					Set = (from s in _setCollection.AsQueryable()
						where s.Id == card.SetId
						select s.Name).FirstOrDefault(),

					SpellSchoolId = card.SpellSchoolId,

					Rarity = (from r in _rarityCollection.AsQueryable()
						where r.Id == card.RarityId
						select r.Name).FirstOrDefault(),

					Health = card.Health,
					Attack = card.Attack,
					ManaCost = card.ManaCost,
					Artist = card.Artist,
					Text = card.Text,
					FlavorText = card.FlavorText
				});
			}
			return result;
		}
	}
}
