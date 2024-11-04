using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2_BED.Properties.Models
{
	public class ExpenseCreate
	{
		public long ModelId { get; set; }
		public long JobId { get; set; }
		[Column(TypeName = "date")]
		public DateTime Date { get; set; }
		public string? Text { get; set; }
		[Column(TypeName = "decimal(9,2)")]
		public decimal amount { get; set; }
	}
}
