using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using EL.Models;
using DAL.Context;
using System.Data.Entity;

namespace DAL.Services
{
    public class GroepslessenService : IGroepslessenService
    {
        public List<Groepsles> AlleGroepslessenWeergeven()
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //alle groepslessen teruggeven
                return ctx.Groepslessen.Where(groepsles => groepsles.Actief == true).ToList();
            }
        }
        public Groepsles GroepslesWeergeven(int? id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //groepsles weergeven waarvan het id gelijk is aan het meegegeven id
                return ctx.Groepslessen.Find(id);
            }
        }

        public List<Groepsles> GroepslessenPerClubWeergeven(int clubId)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //alle groepslessen weergeven van een bepaalde club a.d.h.v. het clubid
                return (from groepsles in ctx.Groepslessen
                        from club in ctx.Clubs
                        where club.ClubId == clubId
                        select groepsles).ToList();
            }
        }

        public void GroepslesToevoegen(Groepsles groepsles)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //meegegeven groepsles aan de database toevoegen
                ctx.Groepslessen.Add(groepsles);

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void GroepslesVerwijderen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //groepsles waarvan het id gelijk is aan het meegegeven id verwijderen
                ctx.Groepslessen.Remove(ctx.Groepslessen.Find(id));

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void groepslesWijzigen(Groepsles groepsles)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //de meegegeven groepsles wijzigen
                ctx.Entry(groepsles).State = EntityState.Modified;

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }
    }
}
