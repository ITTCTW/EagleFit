using EL.Models;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    interface IAdressenService
    {
        void AdresToevoegen(Adres adres);
        void AdresVerwijderen(int id);
        void AdresWijzigen(Adres adres);
        List<Adres> AlleAdressenWeergeven();
        Adres AdresWeergeven(int? id);
        int AdresIdBepalen(int lidNummer);
    }
}
