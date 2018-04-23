using EL.Models;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    interface IAbonnementenService
    {
        void AbonnementToevoegen(Abonnement abonnement);
        void AbonnementVerwijderen(int id);
        void AbonnementWijzigen(Abonnement abonnement);
        List<Abonnement> AlleAbonnementenWeergeven();
        Abonnement AbonnementWeergeven(int? abonnementId);
        Abonnement AbonnementVanEenLidWeergeven(string userId);
    }
}
