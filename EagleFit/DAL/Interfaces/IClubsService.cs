using EL.Models;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    interface IClubsService
    {
        void ClubToevoegen(Club club);
        void ClubVerwijderen(int id);
        void ClubWijzigen(Club club);
        List<Club> AlleClubsWeergeven();
        Club ClubWeergeven(int? id);
        int ClubIdBepalen(int clubId);
    }
}
