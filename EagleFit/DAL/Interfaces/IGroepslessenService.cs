using EL.Models;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    interface IGroepslessenService
    {
        void GroepslesToevoegen(Groepsles groepsles);
        void GroepslesVerwijderen(int id);
        void groepslesWijzigen(Groepsles groepsles);
        List<Groepsles> AlleGroepslessenWeergeven();
        Groepsles GroepslesWeergeven(int? id);
        List<Groepsles> GroepslessenPerClubWeergeven(int clubId);
    }
}
