using EL.Models;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    interface IMedewerkersService
    {
        List<Medewerker> AlleActieveMedewerkersWeergeven();
        Medewerker MedewerkerWeergeven(int id);
        void MedewerkerToevoegen(Medewerker medewerker);
        void MedewerkerWijzigen(Medewerker medewerker);
        void MedewerkerVerwijderen(int id);
        int MedewerkersIdBepalen(int id);
        int MedewerkersIdAdhvPersoonsIdWeergeven(int id);
        List<Persoon> AllePersonenMedewerkersWeergeven();
    }
}
