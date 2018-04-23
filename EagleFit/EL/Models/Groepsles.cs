using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.Models
{
    [Table("Groepslessen")]
    public class Groepsles
    {
        [Key]
        public int GroepslesId { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Naam van de groepsles mag niet meer dan 100 tekens bevatten")]
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public List<Club> Clubs { get; set; }
        public List<Lid> Leden { get; set; }
        public bool Actief { get; set; }
    }
}
