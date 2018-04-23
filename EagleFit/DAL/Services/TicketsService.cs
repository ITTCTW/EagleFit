using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using EL.Models;
using DAL.Context;
using System.Data.Entity;

namespace DAL.Services
{
    public class TicketsService : ITicketsService
    {
        public List<Ticket> AlleTicketsWeergeven()
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //alle tickets weergeven waarvan het proppertie actief op true staat
                return ctx.Tickets.Where(ticket => ticket.Actief == true).ToList();
            }
        }

        public Ticket TicketWeergeven(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //return een ticket a.d.h.v. het meegegeven id
                return ctx.Tickets.Find(id);
            }
        }

        public int TicketIdBepalen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //indien er een ticket in de database zit:
                if (ctx.Tickets.Count() != 0)
                {
                    //het laatste ticket weergeven
                    Ticket laatsteTicket = ctx.Tickets.OrderBy(ticket => ticket.TicketId).FirstOrDefault();

                    //het id van het laatste ticket weergeven
                    int laatsteId = laatsteTicket.TicketId;

                    //1 optellen bij het laatste id en dit teruggeven
                    return ++laatsteId;
                }
                else
                {
                    return 1;
                }
            }
        }

        public void TicketToevoegen(Ticket ticket)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //het meegegeven ticket aan de database toevoegen
                ctx.Tickets.Add(ticket);

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void TicketVerwijderen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //het ticket waarvan het id gelijk is aan het meegegeven id verwijderen
                ctx.Tickets.Remove(ctx.Tickets.Find(id));

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void TicketWijzigen(Ticket ticket)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //het meegegeven ticket wijzigen
                ctx.Entry(ticket).State = EntityState.Modified;

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }
    }
}
