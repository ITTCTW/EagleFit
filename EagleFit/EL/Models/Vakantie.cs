using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.Models
{
    [Table("Vakanties")]
    public class Vakantie
    {
        [Key]
        public int VakantieId { get; set; }
        [Required]
        public int MedewerkersId { get; set; }
        [Required]
        [Display(Name = "Begindatum")]
        public DateTime BeginDatum { get; set; }
        [Required]
        [Display(Name = "Einddatum")]
        public DateTime EindDatum { get; set; }
        public bool Goedgekeurd { get; set; }
    }
}
