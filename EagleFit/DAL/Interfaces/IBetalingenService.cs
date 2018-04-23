using EL.Models;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IBetalingenService
    {
        List<Betaling> AlleBetalingenVanLid(string userId);
        List<Betaling> AlleBetalingenWeergeven();
        Betaling BetalingWeergeven(int id);
        void BetalingToevoegen(Betaling betaling);
        void BetalingWijzigen(Betaling betaling);
        void BetalingVerwijderen(int id);
        int BetalingsIdBepalen(int id);
    }
}
