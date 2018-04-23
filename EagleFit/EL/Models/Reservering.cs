using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.Models
{
    [Table("Reserveringen")]
    public class Reservering
    {
        [Key]
        public int ReserveringsId { get; set; }
        [Required]
        public int ZaalId { get; set; }
        public Zaal Zaal { get; set; }
        [Required]
        public int MedewerkersId { get; set; }
        public Medewerker Medewerker { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        
        public DateTime Reserveringsdatum { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan Reserveringsuur { get; set; }
        public bool Actief { get; set; }
    }
}
