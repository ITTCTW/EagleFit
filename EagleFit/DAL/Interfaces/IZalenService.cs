using EL.Models;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    interface IZalenService
    {
        List<Zaal> AlleZalenWeergeven();
        Zaal ZaalWeergeven(int id);
        void ZaalToevoegen(Zaal zaal);
        void ZaalWijzigen(Zaal zaal);
        void ZaalVerwijderen(int id);
        int ZaalIdBepalen(int id);
    }
}
