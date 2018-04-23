using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.Models
{
    [Table("ToestelProblemen")]
    public class ToestelProbleem
    {
        [Key]
        public int ProbleemId { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Naam mag niet meer dan 100 tekens bevatten")]
        public string Naam { get; set; }
        [Required]
        public string Omschrijving { get; set; }
        public int ToestelId { get; set; }
    }
}
