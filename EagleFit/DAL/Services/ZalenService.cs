using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using EL.Models;
using DAL.Context;
using System.Data.Entity;

namespace DAL.Services
{
    public class ZalenService : IZalenService
    {
        public List<Zaal> AlleZalenWeergeven()
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //alle zalen teruggeven waarvan het proppertie actief op true staat
                return ctx.Zalen.Where(zaal => zaal.Actief == true).ToList();
            }
        }

        public Zaal ZaalWeergeven(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //een zaal weergeven a.d.h.v. het id
                return ctx.Zalen.Find(id);
            }
        }

        public int ZaalIdBepalen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //indien er een zaal in de database zit:
                if(ctx.Zalen.Count() != 0)
                {
                    //de laatste zaal weergeven
                    Zaal laatsteZaal = ctx.Zalen.OrderBy(zaal => zaal.ZaalId).FirstOrDefault();

                    //het id van de laatste zaal weergeven
                    int laatsteZaalId = laatsteZaal.ZaalId;

                    //1 optellen bij het laatste id en dit teruggeven 
                    return ++laatsteZaalId;
                }

                //indien er nog geen zaal in de database zit 1 teruggeven
                else
                {
                    return 1;
                }
            }
        }

        public void ZaalToevoegen(Zaal zaal)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //de meegegeven zaal toevoegen aan de database
                ctx.Zalen.Add(zaal);

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void ZaalVerwijderen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //de zaal waarvan het id gelijk is aan het meegegeven id verwijderen
                ctx.Zalen.Remove(ctx.Zalen.Find(id));

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void ZaalWijzigen(Zaal zaal)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //de meegegeven zaal wijzigen
                ctx.Entry(zaal).State = EntityState.Modified;

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }
    }
}
