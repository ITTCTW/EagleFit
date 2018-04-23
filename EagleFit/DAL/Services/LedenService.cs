using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using EL.Models;
using DAL.Context;
using System.Data.Entity;

namespace DAL.Services
{
    public class LedenService : ILedenService
    {
        public List<Lid> AlleLedenWeergeven()
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //alle leden teruggeven waarvan het proppertie actief op true staat
                return ctx.Leden.Where(lid => lid.Actief == true).ToList();
            }
        }

        public Lid LidWeergeven(int? id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //lid teruggeven a.d.h.v.het id/lidnummer
                return ctx.Leden.Find(id);
            }
        }

        public void LidToevoegen(Lid lid)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //het meegegeven lid toevoegen aan de database
                ctx.Leden.Add(lid);

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void LidVerwijderen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //het lid waarvan het lidnummer gelijk is aan het meegegeven id verwijderen
                ctx.Leden.Remove(ctx.Leden.Find(id));

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void LidWijzigen(Lid lid)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //het meegegeven lid wijzigen
                ctx.Entry(lid).State = EntityState.Modified;

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }
        public int LidNummerBepalen(int lidNummer)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //indien er al een lid in de database zit:
                if(ctx.Leden.Count() != 0)
                {
                    //het laatste lid in de database weergeven
                    Lid id = ctx.Leden.OrderByDescending(lid => lid.LidNummer).FirstOrDefault();

                    //het lidnummer van het laatste lid weergeven
                    int nieuwLidnummer = id.LidNummer;

                    //1 optellen bij dit laatste lidnummer en dit teruggeven
                    return ++nieuwLidnummer;
                }

                //indien er nog geen lid in de database zit 1 teruggeven
                else
                {
                    return 1;
                }
            }
        }

        public int LidnummerMetPersoonsIdWeergeven(int persoonsId)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //lid weergeven a.d.h.v. het persoonsid en dit ook teruggeven
                Lid weerTeGevenLid = ctx.Leden.Where(lid => lid.PersoonId == persoonsId).FirstOrDefault();
                return weerTeGevenLid.LidNummer;
            }
        }

        public Lid LidWeergeven(string userId)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //database connectie openen die automatisch gaat sluiten
                using (ApplicationDbContext ctxDB = new ApplicationDbContext())
                {
                    //user ophalen a.d.h.v. het userid
                    ApplicationUser user = ctxDB.Users.Find(userId);

                    //lid ophalen en teruggeven a.d.h.v. het persoonsid van de user
                    Lid opTeHalenLid = ctx.Leden.Where(lid => lid.PersoonId == user.PersoonsId).FirstOrDefault();
                    return opTeHalenLid;
                }
            }
        }
    }
}
