using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using EL.Models;
using DAL.Context;
using System.Data.Entity;

namespace DAL.Services
{
    public class ToestellenService : IToestellenService
    {
        public List<ToestelProbleem> AlleProblemenWeergevenVanToestel(int toestelId)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //alle problemen teruggeven van het toestel waarvan het id gelijk is aan het meegegeven id
                return ctx.ToestelProblemen.Where(toestel => toestel.ToestelId == toestelId).ToList();
            }
        }

        public ToestelProbleem EenProbleemWeergeven(int probleemId)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //een probleem teruggeven a.d.h.v. het meegegeven id
                return ctx.ToestelProblemen.Find(probleemId);
            }
        }

        public List<Toestel> AlleToestellenWeergeven()
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                try
                {
                    //alle toestellen weergeven waarvan proppertie actief op true staat
                    return ctx.Toestellen.Where(toestel => toestel.Actief == true).ToList();
                }

                //indien er iets misging geven we null terug
                catch
                {
                    return null;
                }
                                 
            }
        }

        public Toestel ToestelWeergeven(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //een toestel weergeven a.d.h.v. het meegegeven id
                return ctx.Toestellen.Find(id);
            }
        }

        public int probleemIdBepalen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //indien er een probleem in de database zit:
                if (ctx.ToestelProblemen.Count() != 0)
                {
                    //het laatste probleem weergeven
                    ToestelProbleem laatsteProbleem = ctx.ToestelProblemen.OrderByDescending(p => p.ProbleemId).FirstOrDefault();

                    //het id van het laatste probleem weergeven
                    int laatsteProbleemId = laatsteProbleem.ProbleemId;

                    //1 optellen bij het laatste id en dit teruggeven
                    return ++laatsteProbleemId;
                }
                else
                {
                    return 1;
                }
            }
        }

        public void ProbleemToevoegen(ToestelProbleem probleem)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //het meegegeven probleem toevoegen aan de database
                ctx.ToestelProblemen.Add(probleem);

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void ProbleemWijzigen(ToestelProbleem probleem)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //het meegegeven probleem wijzigen
                ctx.Entry(probleem).State = EntityState.Modified;

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public int ToestelIdBepalen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //indien het id 0 is:
                if (id == 0)
                {
                    //indien er een toestel in de database zit:
                    if (ctx.Toestellen.Count() != 0)
                    {
                        //het laatste toestel weergeven
                        Toestel laatsteToestel = ctx.Toestellen.OrderByDescending(t => t.ToestelId).FirstOrDefault();

                        //het id van het laatste toestel weergeven
                        int nieuwId = laatsteToestel.ToestelId;

                        //1 optellen bij het laatste id en dit teruggeven
                        return ++nieuwId;
                    }

                    //indien er geen toestel in de database zit 1 teruggeven
                    else
                    {
                        return 1;
                    }
                }

                //indien er al een id was gaan we datzelfde id teruggeven
                else
                {
                    return id;
                }
            }
        }

        public void ToestelToevoegen(Toestel toestel)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //het meegegeven toestel toevoegen aan de database
                ctx.Toestellen.Add(toestel);

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void ToestelVerwijderen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //het toestel waarvan het id gelijk is aan het meegegeven id verwijderen
                ctx.Toestellen.Remove(ctx.Toestellen.Find(id));

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void ToestelWijzigen(Toestel toestel)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //het meegegeven toestel wijzigen
                ctx.Entry(toestel).State = EntityState.Modified;

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }
    }
}
