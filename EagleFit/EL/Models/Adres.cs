using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.Models
{
    [Table("Adressen")]
    public class Adres
    {
        [Key]
        public int AdresId { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "straat mag niet meer dan 100 tekens zijn")]
        public string Straat { get; set; }
        [Required]
        public int Huisnummer { get; set; }
        [Required]
        [Range(1000, 9999)]
        public int Postcode { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Gemeente mag niet meer dan 100 tekens zijn")]
        public string Gemeente { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int ClubId { get; set; }
        public List<Lid> Leden { get; set; }
        public string Beschrijving { get; set; }
        public bool Actief { get; set; }
        public Adres()
        {
        }
    }
}
