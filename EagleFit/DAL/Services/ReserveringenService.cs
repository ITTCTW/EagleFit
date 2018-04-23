using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using EL.Models;
using DAL.Context;
using System.Data.Entity;

namespace DAL.Services
{
    public class ReserveringenService : IReserveringenService
    {
        public List<Reservering> AlleReserveringenWeergeven()
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //alle reserveringen teruggeven
                return ctx.Reserveringen.ToList();
              
            }
        }

        public Reservering ReserveringWeergeven(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //een reservering teruggeven a.d.h.v. het meegegeven id
                return ctx.Reserveringen.Find(id);
            }
        }

        public int ReserveringsIdBepalen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //indien er een reservering in de database zit:
                if(ctx.Reserveringen.Count() != 0)
                {
                    //laatste reservering weergeven
                    Reservering laatsteReservering = ctx.Reserveringen.OrderBy(reservering => reservering.ReserveringsId).FirstOrDefault();

                    //het id van de laatste reservering weergeven
                    int laatsteReserveringsId = laatsteReservering.ReserveringsId;
                    return ++laatsteReserveringsId;
                }

                //indien er geen reservering in de database zit geef 1 terug
                else
                {
                    return 1;
                }
                
            }
        }

        public void ReserveringToevoegen(Reservering reservering)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //de meegegeven reservering toevoegen aan de database
                ctx.Reserveringen.Add(reservering);

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void ReserveringVerwijderen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //de reservering waarvan het id gelijk is aan het meegegeven id verwijderen
                ctx.Reserveringen.Remove(ctx.Reserveringen.Find(id));

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void ReserveringWijzigen(Reservering reservering)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //de meegegeven reservering wijzigen
                ctx.Entry(reservering).State = EntityState.Modified;

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public List<Reservering> AlleActieveReserveringenWeergeven()
        {
                //een lijst met toekomstige reserveringen aanmaken/instantiëren
                List<Reservering> toekomstigeReserveringen = new List<Reservering>();

                //database connectie openen die automatisch gaat sluiten
                using (EagleFitContext ctx = new EagleFitContext())
                {
                    //alle reserveringen weergeven
                    List<Reservering> alleReserveringen = ctx.Reserveringen.ToList();

                    //indien er reserveringen zijn:
                    if (alleReserveringen != null)
                    {
                        //als de reservering in de lijst met alle reserveringen op vandaag of in de toekomst ligt, gaan we die reservering toevoegen aan de lijst met toekomstige reserveringen
                        foreach (var item in ctx.Reserveringen.ToList())
                        {
                            if (item.Reserveringsdatum >= DateTime.Now.Date && item.Actief == true)
                            {
                                toekomstigeReserveringen.Add(item);
                            }
                        }
                    }

                    //de toekomstige reserveringen teruggeven               
                    return toekomstigeReserveringen;
            }
        }

        public List<Reservering> AlleReserveringenPerZaalWeergeven(int zaalId)
        {
            //lijst met toekomstige reserveringen aanmaken
            List<Reservering> toekomstigeReserveringen = new List<Reservering>();

            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //lijst met alle reserveringen
                List<Reservering> alleReserveringen = ctx.Reserveringen.ToList();
                if (alleReserveringen != null)
                {
                    //elke reservering uit de lijst met alle reserveringen die vandaag of in de toekomst ligt en waarvan het id van de zaal gelijk is aan het meegegeven id toevoegen aan lijst met toekomstige reserveringen
                    foreach (var item in ctx.Reserveringen.ToList())
                    {
                        if (item.Reserveringsdatum.Date >= DateTime.Now.Date && item.ZaalId == zaalId)
                        {
                            toekomstigeReserveringen.Add(item);
                        }
                    }
                }

                //de toekomstige reserveringen teruggeven
                return toekomstigeReserveringen;
            }
        }
    }
}
