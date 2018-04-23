using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.Models
{
    [Table("Zalen")]
    public class Zaal
    {
        [Key]
        public int ZaalId { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Zaalnaam mag niet meer dan 100 tekens bevatten")]
        public string Zaalnaam { get; set; }
        [Required]
        [Display(Name = "Grootte in m²")]
        public decimal GrooteInVierkanteMeter { get; set; }
        public int ClubId { get; set; }
        public Club Club { get; set; }
        public bool Actief { get; set; }
    }
}
