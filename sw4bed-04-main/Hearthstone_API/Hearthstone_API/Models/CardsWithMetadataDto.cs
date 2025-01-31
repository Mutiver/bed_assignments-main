﻿using System.Text.Json.Serialization;

namespace Hearthstone_API.Services
{
	public class CardsWithMetadataDto
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Class { get; set; }
		public string? CardType { get; set; }
		public string? Set { get; set; }
		public int? SpellSchoolId { get; set; }
		public string? Rarity { get; set; }
		public int? Health { get; set; }
		public int? Attack { get; set; }
		public int ManaCost { get; set; }
		public string? Artist { get; set; }
		public string? Text { get; set; }
		public string? FlavorText { get; set; }
	}
}