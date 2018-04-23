using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using EL.Models;
using DAL.Context;
using System.Data.Entity;

namespace DAL.Services
{
    public class PersonenService : IPersonenService
    {
        public List<Persoon> AllePersonenWeergeven()
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //alle personen teruggeven
                return ctx.Personen.ToList();
            }
        }

        public Persoon PersoonWeergeven(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //een persoon teruggeven a.d.h.v. het meegegeven id
                return ctx.Personen.Find(id);
            }
        }
        public Persoon PersoonWeergeven(string userId)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //database connectie openen die automatisch gaat sluiten
                using (ApplicationDbContext ctxDB = new ApplicationDbContext())
                {
                    //user ophalen a.d.h.v. meegegeven userid
                    ApplicationUser user = ctxDB.Users.Find(userId);

                    //een persoon teruggeven a.d.h.v. het persoonsid van de user
                    return ctx.Personen.Find(user.PersoonsId);
                }
                    
            }
        }

        public int PersoonIdBepalen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //indien er een persoon in de database zit:
                if (ctx.Personen.Count() != 0)
                {
                    //laatste persoon ophalen
                    var LaatstePersoon = ctx.Personen.OrderByDescending(persoon => persoon.PersoonsId).FirstOrDefault();

                    //het id van de laatste persoon ophalen
                    int nieuwId = LaatstePersoon.PersoonsId;
                    
                    //1 optellen bij het laatste id en dit teruggeven
                    return ++nieuwId;
                }

                //indien er nog geen persoon in de database zit geven we 1 terug
                else
                {
                    return 1;
                }
            }
        }

        public void PersoonToevoegen(Persoon persoon)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //de meegegeven persoon toevoegen aan de database
                ctx.Personen.Add(persoon);

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void PersoonWijzigen(Persoon persoon)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //de meegegeven persoon wijzigen
                ctx.Entry(persoon).State = EntityState.Modified;

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }
    }
}
