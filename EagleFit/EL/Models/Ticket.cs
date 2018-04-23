using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Titel mag niet meer dan 100 tekens bevatten")]
        public string Titel { get; set; }
        [Required]
        public string Klacht { get; set; }
        [Required]
        public bool Actief { get; set; }
    }
}
