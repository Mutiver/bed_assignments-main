using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2_BED.Models
{
    public class Expense
    {
        [Key]
        public long ExpenseId { get; set; }
        [ForeignKey("Id")]
        public long ModelId { get; set; }
		[ForeignKey("Id")]
		public long JobId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public string? Text { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal amount { get; set; }
    }
}
