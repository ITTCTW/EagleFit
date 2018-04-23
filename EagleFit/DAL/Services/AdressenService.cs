using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using EL.Models;
using DAL.Context;
using System.Data.Entity;

namespace DAL.Services
{
    public class AdressenService : IAdressenService
    {
        public void AdresToevoegen(Adres adres)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //adres toevoegen aan database
                ctx.Adressen.Add(adres);

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void AdresVerwijderen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //adres verwijderen waarvan het id hetzelfde is als de id die meegegeven is
                ctx.Adressen.Remove(ctx.Adressen.Find(id));

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void AdresWijzigen(Adres adres)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //adres wijzigen dat meegegeven is
                ctx.Entry(adres).State = EntityState.Modified;

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public List<Adres> AlleAdressenWeergeven()
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //alle addressen teruggeven waarvan actief proppertie op true staat
                return ctx.Adressen.Where(adres => adres.Actief == true).ToList();
            }
        }

        public Adres AdresWeergeven(int? id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //een adres teruggeven a.d.h.v. het id
                return ctx.Adressen.Find(id);
            }
        }

        public List<Adres> AlleAdressenClubs()
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //alle adressen teruggeven van alle clubs die er zijn 
                return ctx.Adressen.Where(a => a.ClubId != 0).ToList();
            }
            
        }

        public int AdresIdBepalen(int adresId)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //Indien er al een adres in de database zit:
                if (ctx.Adressen.Count() != 0)
                {
                    //Het laatste adres in de database weergeven
                    Adres laatsteAdres = ctx.Adressen.OrderByDescending(adres => adres.AdresId).FirstOrDefault();

                    //Het id van het laatste adres weergeven
                    int laatsteAdresId = laatsteAdres.AdresId;

                    //1 bij het laatste id optellen en dit teruggeven
                    return ++laatsteAdresId;
                }

                //Indien er nog geen adres bestaan 1 teruggeven 
                else
                {
                    return 1;
                }
            }
                
        }
    }
}
