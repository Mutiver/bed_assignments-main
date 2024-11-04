using Hearthstone_API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;
using Hearthstone_API;


namespace Hearthstone_API.Services
{

	public class SeedData
	{

		private IMongoCollection<Card> _cardCollection;
		private IMongoCollection<Class> _classCollection;
		private IMongoCollection<Set> _setCollection;
		private IMongoCollection<CardType> _cardTypeCollection;
		private IMongoCollection<Rarity> _rarityCollection;
		public SeedData(IOptions<MongoDbSettings> mongoDbSettings)
		{
			MongoClient client = new MongoClient(mongoDbSettings.Value.ConnectionURI);
			IMongoDatabase database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);

			_cardCollection = database.GetCollection<Card>(mongoDbSettings.Value.CardCollectionName);
			_cardTypeCollection = database.GetCollection<CardType>(mongoDbSettings.Value.CardTypeCollectionName);
			_classCollection = database.GetCollection<Class>(mongoDbSettings.Value.ClassCollectionName);
			_rarityCollection = database.GetCollection<Rarity>(mongoDbSettings.Value.RarityCollectionName);
			_setCollection = database.GetCollection<Set>(mongoDbSettings.Value.SetCollectionName);

		}

		//get functions


		public void CreateCards()
		{
			if (_cardCollection.Find(c => true).Any())
				return;

			foreach (var path in new[] { "cards.json" })
			{
				using var file = new StreamReader(path);
				var cards = JsonSerializer.Deserialize<List<Card>>(file.ReadToEnd(), new JsonSerializerOptions
				{ PropertyNameCaseInsensitive = true });

				_cardCollection.InsertMany(cards);
			}
		}

		public void CreateClasses()
		{
			if (_classCollection.Find(c => true).Any())
				return;

			foreach (var path in new[] { "metadata.json" })
			{
				using var file = new StreamReader(path);
				var metadata = JsonSerializer.Deserialize<Metadata>(file.ReadToEnd(), new JsonSerializerOptions
				{ PropertyNameCaseInsensitive = true });

				if (metadata == null || metadata.Classes == null)
					return;

				_classCollection.InsertMany(metadata.Classes);
			}
		}

		public void CreateRarities()
		{
			if (_rarityCollection.Find(r => true).Any())
				return;

			foreach (var path in new[] { "metadata.json" })
			{
				using var file = new StreamReader(path);
				var metadata = JsonSerializer.Deserialize<Metadata>(file.ReadToEnd(), new JsonSerializerOptions
				{ PropertyNameCaseInsensitive = true });

				if (metadata == null || metadata.Rarities == null)
					return;

				_rarityCollection.InsertMany(metadata.Rarities);
			}
		}

		public void CreateSets()
		{
			if (_setCollection.Find(c => true).Any())
				return;

			foreach (var path in new[] { "metadata.json" })
			{
				using var file = new StreamReader(path);
				var metadata = JsonSerializer.Deserialize<Metadata>(file.ReadToEnd(), new JsonSerializerOptions
				{ PropertyNameCaseInsensitive = true });

				if (metadata == null || metadata.Sets == null)
					return;

				_setCollection.InsertMany(metadata.Sets);
			}
		}

		public void CreateCardTypes()
		{
			if (_cardTypeCollection.Find(c => true).Any())
				return;

			foreach (var path in new[] { "metadata.json" })
			{
				using var file = new StreamReader(path);
				var metadata = JsonSerializer.Deserialize<Metadata>(file.ReadToEnd(), new JsonSerializerOptions
				{ PropertyNameCaseInsensitive = true });

				if (metadata == null || metadata.Types == null)
					return;

				_cardTypeCollection.InsertMany(metadata.Types);
			}
		}

	}
}
