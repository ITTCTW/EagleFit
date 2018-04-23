using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.Models
{
    [Table("Betalingen")]
    public class Betaling
    {
        [Key]
        public int BetalingsId { get; set; }
        [Required]
        public int Lidnummer { get; set; }
        [Required]
        public DateTime Datum { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Bedrag { get; set; }
        [Required]
        public string Omschrijving { get; set; }
        public bool Betaald { get; set; }

    }
}
