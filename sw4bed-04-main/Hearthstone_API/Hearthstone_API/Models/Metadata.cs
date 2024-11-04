namespace Hearthstone_API.Models
{
	public class Metadata
	{
		public List<Set>? Sets { get; set; }
		public List<Rarity>? Rarities { get; set; }
		public List<CardType>? Types { get; set; }
		public List<Class>? Classes { get; set; }
	}
}
