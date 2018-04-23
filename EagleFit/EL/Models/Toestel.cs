using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.Models
{
    [Table("Toestellen")]
    public class Toestel
    {
        [Key]
        public int ToestelId { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Naam mag niet meer dan 100 tekens bevatten")]
        public string Naam { get; set; }
        [Display(Name = "Aankoopdatum")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime AankoopDatum { get; set; }
        [DataType(DataType.Upload)]
        public string Handleiding { get; set; }
        [Required]
        public Kuisproducten Kuisproduct { get; set; }
        public bool Actief { get; set; }
    }
}
