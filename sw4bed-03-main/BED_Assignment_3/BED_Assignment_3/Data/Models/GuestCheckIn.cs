using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace BED_Assignment_3.Data.Models
{
    public class GuestCheckIn
    {
        [Key]
        public int Id { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int RoomNr { get; set; }
        public DateTime Date { get; set; } 
    }
}
