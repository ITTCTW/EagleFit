using EL.Models;
using System.Collections.Generic;

namespace EL.ViewModels
{
    public class LedenAanmakenVM
    {
        public Lid MyLid { get; set; }
        public int AbonnementId { get; set; }
        public List<Club> Clubs { get; set; }
        public List<Abonnement> Abonnementen { get; set; }

    }
}
