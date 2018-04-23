using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.Models
{
    [Table("Personen")]
    public class Persoon
    {
        [Key]
        public int PersoonsId { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Voornaam mag niet meer dan 100 tekens bevatten")]
        public string Voornaam { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Familienaam mag niet meer dan 100 tekens bevatten")]
        public string Familienaam { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "Telefoonnummer mag niet meer dan 20 tekens bevatten")]
        [Display(Name = "Telefoonnummer")]
        public string TelefoonNummer { get; set; }
        [Required]
        [MaxLength(5, ErrorMessage = "Geslacht mag niet meer dan 5 tekens bevatten")]
        public string Geslacht { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "e-mailadres mag niet meer dan 100 tekens bevatten")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public Adres Adres { get; set; }
        public int AdresId { get; set; }
        public string Naam
        {
            get
            {
                return Voornaam + " " + Familienaam;
            }
                
        }

    }
}