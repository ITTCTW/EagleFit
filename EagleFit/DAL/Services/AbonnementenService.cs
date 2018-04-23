using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using EL.Models;
using DAL.Context;
using System.Data.Entity;

namespace DAL.Services
{
    public class AbonnementenService : IAbonnementenService
    {
        public void AbonnementToevoegen(Abonnement abonnement)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //abonnement toevoegen aan de database
                ctx.Abonnementen.Add(abonnement);

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void AbonnementVerwijderen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //abonnement waarvan het id gelijk is aan hetgeen we hier meegeven verwijderen
                ctx.Abonnementen.Remove(ctx.Abonnementen.Find(id));

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void AbonnementWijzigen(Abonnement abonnement)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //abonnement dat meegegeven is wijzigen
                ctx.Entry(abonnement).State = EntityState.Modified;

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public List<Abonnement> AlleAbonnementenWeergeven()
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //alle abonnementen weergeven waarvan het actief proppertie op true staat
                return ctx.Abonnementen.Where(abonnement => abonnement.Actief == true).ToList();
            }
        }

        public Abonnement AbonnementVanEenLidWeergeven(string userId)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //database connectie openen die automatisch gaat sluiten
                using (ApplicationDbContext ctxDB = new ApplicationDbContext())
                {
                    //user ophalen a.d.h.v. userId
                    ApplicationUser user = ctxDB.Users.Find(userId);

                    //lid ophalen a.d.h.v. PersoonsId die we vinden in de user
                    Lid lid = ctx.Leden.Find(user.PersoonsId);

                    //abonnement weergeven van dit lid
                    return ctx.Abonnementen.Find(lid.AbonnementId);                    
                }               
            }
        }

        public Abonnement AbonnementWeergeven(int? AbonnementId)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //abonnement weergeven a.d.h.v. meegegeven id
                return ctx.Abonnementen.Find(AbonnementId);
            }
        }
    }
}
