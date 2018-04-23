using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.Models
{
    [Table("Medewerkers")]
    public class Medewerker
    {
        [Key]
        public int MedewerkersId { get; set; }
        public int PersoonsId { get; set; }
        public Persoon Persoon { get; set; }
        public decimal Salaris { get; set; }
        [MaxLength(128,ErrorMessage = "status mag niet meer dan 128 tekens bevatten")]
        [Display(Name = "Medewerksstatus")]
        public string MedewerkersStatus { get; set; }
        public bool Actief { get; set; }
    }
}
