using DAL.Services;
using EL.Models;
using GoogleMaps.LocationServices;
using System.Web;
using System.Web.Hosting;

namespace EL.BLServices
{
    public class BusinessServices
    {
        //DAL Services oproepen zodat deze gebruikt kunnen worden in deze klasse
        private AdressenService adressenService = new AdressenService();
        private ClubsService clubsService = new ClubsService();
        public Adres AdresAanmakenMetLatLng(Adres adres, Club club)
        {
            //Indien het adresid 0 is gaan we naar de methode om het id te bepalen
            if (adres.AdresId == 0)
                adres.AdresId = adressenService.AdresIdBepalen(adres.AdresId);

            //Hier gaan we een service gebruiken om latitude en longitude van ons adres terug te krijgen 
            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress($"{adres.Straat} {adres.Huisnummer} {adres.Postcode}");

            //hier gaan we de latitude, longitude en beschrijving aan de propperties van ons adresobject toekennen
            adres.Latitude = point.Latitude;
            adres.Longitude = point.Longitude;
            adres.Beschrijving = club.Naam;

            //adres terugsturen
            return adres;
        }

        public Club ClubAanmaken(Club club, Adres adres, HttpPostedFileBase file)
        {
            //indien clubid 0 is gaan we naar de methode om een id te laten bepalen
            if(club.ClubId == 0)
                club.ClubId = clubsService.ClubIdBepalen(club.ClubId);

            //adresid toekennen aan de club
            club.AdresId = adres.AdresId;

            //File upload ophalen en opslaan
            string pathToSave = HostingEnvironment.MapPath("~/Images/GroepslesPlanningen/");
            string filename = file.FileName;
            string handleidingPath = HostingEnvironment.MapPath($"~/Images/GroepslesPlanningen/{filename}");
            file.SaveAs(handleidingPath);

            //File toekennen aan de club
            club.Kalender = handleidingPath;

            //club actief zetten
            club.Actief = true;

            //Naar methode verwijzen om de club toe te voegen aan de database
            clubsService.ClubToevoegen(club);

            //club terugsturen
            return club;
        }
    }
}
