using EL.Models;
using System.Collections.Generic;

namespace EL.ViewModels
{
    public class AdresMetClubsVM
    {
        public Adres Adres { get; set; }
        public Club Club { get; set; }
        public List<Club> AlleClubs { get; set; }
        public bool chkbxClub { get; set; }
    }
}
