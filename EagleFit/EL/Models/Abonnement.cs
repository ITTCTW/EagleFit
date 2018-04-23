using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.Models
{
    [Table("Abonnementen")]
    public class Abonnement
    {
        [Key]
        public int AbonnementId { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Naam van abonnement mag niet meer dan 100 tekens bevatten")]
        public string Naam { get; set; }
        [Required]
        public string Omschrijving { get; set; }
        [Required]
        [Display(Name = "Prijs per maand")]
        public decimal PrijsPerMaand { get; set; }
        public bool Actief { get; set; }
    }
}
