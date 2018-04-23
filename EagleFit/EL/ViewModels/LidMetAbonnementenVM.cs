using EL.Models;
using System.Collections.Generic;

namespace EL.ViewModels
{
    public class LidMetAbonnementenVM
    {
        public Lid Lid { get; set; }
        public List<Abonnement> AlleAbonnementen { get; set; }
    }
}
