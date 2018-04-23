using EL.Models;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    interface IToestellenService
    {
        List<Toestel> AlleToestellenWeergeven();
        Toestel ToestelWeergeven(int id);
        void ToestelToevoegen(Toestel toestel);
        void ToestelWijzigen(Toestel toestel);
        void ToestelVerwijderen(int id);
        int ToestelIdBepalen(int id);
        ToestelProbleem EenProbleemWeergeven(int probleemId);
        List<ToestelProbleem> AlleProblemenWeergevenVanToestel(int toestelId);
        void ProbleemToevoegen(ToestelProbleem probleem);
        void ProbleemWijzigen(ToestelProbleem probleem);
        int probleemIdBepalen(int id);

    }
}
