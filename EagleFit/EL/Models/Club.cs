using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.Models
{
    [Table("Clubs")]
    public class Club
    {
        [Key]
        public int ClubId { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "Naam van de club mag niet meer dan 200 tekens bevatten")]
        public string Naam { get; set; }
        [Required]
        public int AdresId { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Telefoonnummer mag niet meer dan 20 tekens bevatten")]
        [Display(Name = "Telefoonnummer")]
        public string TelefoonNummer { get; set; }
        [Required]
        [MaxLength(200, ErrorMessage = "Kalendernaam mag niet meer dan 200 tekens bevatten")]
        public string Kalender { get; set; }
        public List<Groepsles> Groepslessen { get; set; }
        public bool Actief { get; set; }
    }
}
