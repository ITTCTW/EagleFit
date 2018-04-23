using EL.Models;
using System.Collections.Generic;

namespace EL.ViewModels
{
    public class BetalingAanmakenVM
    {
        public Betaling Betaling { get; set; }
        public List<Persoon> AlleLeden { get; set; }
        public int PersoonsId { get; set; }
    }
}
