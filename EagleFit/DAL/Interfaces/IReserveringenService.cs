using EL.Models;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    interface IReserveringenService
    {
        List<Reservering> AlleReserveringenWeergeven();
        List<Reservering> AlleReserveringenPerZaalWeergeven(int zaalId);
        Reservering ReserveringWeergeven(int id);
        void ReserveringToevoegen(Reservering reservering);
        void ReserveringWijzigen(Reservering reservering);
        void ReserveringVerwijderen(int id);
        int ReserveringsIdBepalen(int id);
        List<Reservering> AlleActieveReserveringenWeergeven();
    }
}
