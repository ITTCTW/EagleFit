using EL.Models;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    interface ILedenService
    {
        void LidToevoegen(Lid lid);
        void LidVerwijderen(int id);
        void LidWijzigen(Lid lid);
        List<Lid> AlleLedenWeergeven();
        Lid LidWeergeven(int? id);
        int LidNummerBepalen(int lidNummer);
        int LidnummerMetPersoonsIdWeergeven(int persoonsId);
        Lid LidWeergeven(string userId);
    }
}
