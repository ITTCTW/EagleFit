using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.Models
{
    [Table("Leden")]
    public class Lid
    {
        [Key]
        public int LidNummer { get; set; }
        [Required]
        public int PersoonId { get; set; }
        public Persoon Persoon { get; set; }
        [Required]
        public int AbonnementId { get; set; }        
        [Required]
        public int ClubId { get; set; }
        public int TeWijzigenAbonnementId { get; set; }
        public Club Club { get; set; }
        public List<Groepsles> Groepslessen { get; set; }
        public bool Actief { get; set; }

    }
}
