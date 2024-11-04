using MongoDB.Driver;
using MTGCollection.Models;

namespace MTGCollection.Services;

public class DeckService {

  private readonly ILogger<DeckService> _logger;
  private readonly IMongoCollection<Deck> _collection;
  
  public DeckService(ILogger<DeckService> logger, MongoService service) {
    _logger = logger;
    _collection = service.Client.GetDatabase("mtg").GetCollection<Deck>("decks");
  }

  public async Task<IList<Deck>> GetDecks(string? id) {
    var builder = Builders<Deck>.Filter;
    var filter = builder.Empty;
    if(id is not null) {
      filter &= builder.Eq(x => x.Id, id);
    }
    return await _collection.Find(filter).ToListAsync();
  }

  public async void CreateDeck(Deck deck) {
    await _collection.InsertOneAsync(deck);
  }
}