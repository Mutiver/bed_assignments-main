using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Assignment2_BED.Properties.Models
{
	[PrimaryKey(nameof(ExpenseId))]
	public class ExpenseCreated
	{
		[Key]
		public long ExpenseId { get; set; }
		public long ModelId { get; set; }
		public long JobId { get; set; }
		[Column(TypeName = "date")]
		public DateTime Date { get; set; }
		public string? Text { get; set; }
		[Column(TypeName = "decimal(9,2)")]
		public decimal amount { get; set; }
	}
}
