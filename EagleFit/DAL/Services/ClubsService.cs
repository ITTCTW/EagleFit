using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using EL.Models;
using DAL.Context;
using System.Data.Entity;

namespace DAL.Services
{
    public class ClubsService : IClubsService
    {
        public int ClubIdBepalen(int clubId)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //Indien er al een club in de database zit:
                if (ctx.Clubs.Count() != 0)
                {
                    //laatste club ophalen
                    Club laatsteClub = ctx.Clubs.OrderByDescending(club => club.ClubId).FirstOrDefault();

                    //het id van de laatste club ophalen
                    int laatsteClubId = laatsteClub.ClubId;

                    //1 optellen bij dat laatste id en dit teruggeven
                    return ++laatsteClubId;
                }

                //Indien er nog geen clubs in de database zitten gaan we 1 teruggeven
                else
                {
                    return 1;
                }
            }
        }

        public void ClubToevoegen(Club club)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //meegegeven club toevoegen aan database
                ctx.Clubs.Add(club);

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void ClubVerwijderen(int id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //club waarvan het id gelijk is aan het meegegeven id verwijderen
                ctx.Clubs.Remove(ctx.Clubs.Find(id));

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public void ClubWijzigen(Club club)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //de meegegeven club wijzigen
                ctx.Entry(club).State = EntityState.Modified;

                //de aanpassingen opslaan
                ctx.SaveChanges();
            }
        }

        public List<Club> AlleClubsWeergeven()
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //alle clubs in de database teruggeven
                return ctx.Clubs.Where(club => club.Actief == true).ToList();
            }
        }

        public Club ClubWeergeven(int? id)
        {
            //database connectie openen die automatisch gaat sluiten
            using (EagleFitContext ctx = new EagleFitContext())
            {
                //een club teruggeven a.d.h.v. het meegegeven id
                return ctx.Clubs.Find(id);
            }
        }
    }
}
