using EL.Models;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    interface IPersonenService
    {
        List<Persoon> AllePersonenWeergeven();
        Persoon PersoonWeergeven(int id);
        Persoon PersoonWeergeven(string userId);
        int PersoonIdBepalen(int id);
        void PersoonToevoegen(Persoon persoon);
        void PersoonWijzigen(Persoon persoon);
    }
}
