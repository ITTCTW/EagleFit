using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using EL.Models;
using DAL.Context;
using System.Data.Entity;

namespace DAL.Services
{
    public class BetalingenService : IBetalingenService
    {
        //We maken een instantie van de ledenservice klasse in DAL die we private maken om beveiligingsredenen
        private LedenService ledenService = new LedenService();

        public List<Betaling> AlleBetalingenVanLid(string userId)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //database connectie openen die automatisch gaat sluiten
                using (ApplicationDbContext ctxDB = new ApplicationDbContext())
                {
                    //user ophalen a.d.h.v. het id
                    ApplicationUser user = ctxDB.Users.Find(userId);

                    //lid weergeven a.d.h.v. het persoonsid die in de user zit
                    Lid lid = ledenService.LidWeergeven(ledenService.LidnummerMetPersoonsIdWeergeven(user.PersoonsId));

                    //als er betalingen te vinden zijn waarvan het lidnummer in de betaling gelijk is aan het lidnummer van het lid, gaan we al die betalingne teruggeven 
                    if (ctx.Betalingen.Where(betaling => betaling.Lidnummer == lid.LidNummer).ToList().Count != 0)
                        return ctx.Betalingen.Where(betaling => betaling.Lidnummer == lid.LidNummer).ToList();

                    //anders gaan we null teruggeven
                    else
                        return null;
                       
                    
                }
            }
        }

        public int BetalingsIdBepalen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //Indien er al een betaling in de database zit:
                if(ctx.Betalingen.Count() != 0)
                {
                    //laatste betalingen ophalen a.d.h.v. id
                    Betaling laatsteBetaling = ctx.Betalingen.OrderByDescending(b => b.BetalingsId).FirstOrDefault();

                    //het id van de laatste betalingen weergeven
                    int laatsteBetalingsId = laatsteBetaling.BetalingsId;

                    //1 optellen bij het laatste id en dit teruggeven
                    return ++laatsteBetalingsId;
                }

                //Indien er nog niks in de tabel betalingen van de database zit 1 teruggeven
                else
                {
                    return 1;
                }
            }
        }

        public void BetalingToevoegen(Betaling betaling)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //de meegegeven betaling toevoegen aan de database
                ctx.Betalingen.Add(betaling);

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void BetalingVerwijderen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //betaling verwijderen uit de database waarvan het id gelijk is aan het meegegeven id
                ctx.Betalingen.Remove(ctx.Betalingen.Find(id));

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void BetalingWijzigen(Betaling betaling)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //de meegegeven betaling wijzigen
                ctx.Entry(betaling).State = EntityState.Modified;

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public List<Betaling> AlleBetalingenWeergeven()
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //alle betalingen teruggeven
                return ctx.Betalingen.ToList();
            }
        }

        public Betaling BetalingWeergeven(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //een betaling teruggeven a.d.h.v. het meegegeven id
                return ctx.Betalingen.Find(id);
            }
        }
    }
}
