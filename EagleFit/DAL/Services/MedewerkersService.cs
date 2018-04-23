using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using EL.Models;
using DAL.Context;
using System.Data.Entity;
using System;

namespace DAL.Services
{
    public class MedewerkersService : IMedewerkersService
    {
        public List<Medewerker> AlleActieveMedewerkersWeergeven()
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //alle medewerkers weergeven waarvan het proppertie actief op true staat
                return ctx.Medewerkers.Where(medewerker => medewerker.Actief == true).ToList();
            }
        }

        public List<Persoon> AllePersonenMedewerkersWeergeven()
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //alle medewerkers ophalen en in een lijst steken
                List<Medewerker> medewerkers = ctx.Medewerkers.ToList();
                
                //een lijst van personen aanmaken
                List<Persoon> medewerkersPersonen = new List<Persoon>();

                //voor elke medewerker de persoon ophalen en in de lijst van personen steken
                foreach (var item in medewerkers)
                {
                    item.Persoon = ctx.Personen.Find(item.PersoonsId);
                    medewerkersPersonen.Add(item.Persoon);
                }
                
                //de lijst van personen teruggeven
                return medewerkersPersonen;
            }
        }

        public Medewerker MedewerkerWeergeven(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //een medewerker teruggeven a.d.h.v. het meegegeven id
                return ctx.Medewerkers.Find(id);
            }
        }

        public int MedewerkersIdAdhvPersoonsIdWeergeven(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                Medewerker terugTeGevenMedewerker = ctx.Medewerkers.Where(medewerker => medewerker.PersoonsId == id).FirstOrDefault();

                //idnien er een medewerker bij de persoon hoort het id van een medewerker teruggeven a.d.h.v. het persoonsid
                if (terugTeGevenMedewerker == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(terugTeGevenMedewerker.MedewerkersId);
                }
                
            }
        }

        public int MedewerkersIdBepalen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //indien er al een medewerker in de database zit:
                if (ctx.Medewerkers.Count() != 0)
                {
                    //laatste medewerker ophalen
                    Medewerker LaatsteMedewerker = ctx.Medewerkers.OrderByDescending(medewerker => medewerker.MedewerkersId).FirstOrDefault();

                    //het id van die laatste medewerker ophalen
                    int laatsteId = LaatsteMedewerker.MedewerkersId;

                    //1 optellen bij het laatste id en dit teruggeven
                    return ++laatsteId;
                }

                //indien er nog geen medewerker in de database zit 1 teruggeven
                else
                {
                    return 1;
                }
            }
        }

        public void MedewerkerToevoegen(Medewerker medewerker)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //de meegegeven medewerker aan de database toevoegen
                ctx.Medewerkers.Add(medewerker);

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void MedewerkerVerwijderen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //de medewerker waarvan het id gelijk is aan het meegegeven id verwijderen
                ctx.Medewerkers.Remove(ctx.Medewerkers.Find(id));

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void MedewerkerWijzigen(Medewerker medewerker)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //de meegegeven medewerker wijzigen
                ctx.Entry(medewerker).State = EntityState.Modified;

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }
    }
}
