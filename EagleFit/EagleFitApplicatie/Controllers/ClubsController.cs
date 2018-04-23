using System.Web;
using System.Web.Mvc;
using EL.Models;
using DAL.Services;
using EL.ViewModels;
using Ionic.Zip;
using EL.BLServices;

namespace EagleFitApplicatie.Controllers
{
    public class ClubsController : Controller
    {
        private ClubsService clubsService = new ClubsService();
        private AdressenService adressenService = new AdressenService();
        private BusinessServices businessServices = new BusinessServices();

        // GET: Clubs
        public ActionResult Index()
        {
            try
            {
                //de index pagina weergeven met een lijst van alle clubs 
                return View(clubsService.AlleClubsWeergeven());
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // GET: Clubs/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                //de gekozen club weergeven
                Club club = clubsService.ClubWeergeven(id);

                //viewmodel aanmaken met club en adres
                ClubWeergevenVM clubWeergevenVM = new ClubWeergevenVM()
                {
                    Club = club,
                    Adres = adressenService.AdresWeergeven(club.AdresId)
                };

                //de details pagina weergeven met de gegevens in de viewmodel
                return View(clubWeergevenVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // GET: Clubs/Create
        public ActionResult Create()
        {
            try
            {
                //create pagina weergeven met leeg viewmodel
                return View(new ClubWeergevenVM());
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }            
        }

        // POST: Clubs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClubWeergevenVM clubWeergevenVM, HttpPostedFileBase file)
        {
            try
            {
                //adres en club ophalen uit viewmodel
                Adres adres = clubWeergevenVM.Adres;
                Club club = clubWeergevenVM.Club;

                //methode aanspreken om id te bepalen voor club en adres dat we proberen aan te maken
                adres.AdresId = adressenService.AdresIdBepalen(adres.AdresId);
                club.ClubId = clubsService.ClubIdBepalen(club.ClubId);

                //adresgegevens vervolledigen
                adres.ClubId = club.ClubId;
                businessServices.AdresAanmakenMetLatLng(adres, club);
                adres.Actief = true;

                //methode aanspreken om het adres toe te voegen aan de database
                adressenService.AdresToevoegen(adres);

                //methode aanspreken om de club aan te maken en aan de database toe te voegen
                businessServices.ClubAanmaken(club, adres, file);

                //terugsturen naar de index methode
                return RedirectToAction("Index");
            }
            //indien er iets misloopt wordt de create pagina opnieuw weergegeven
            catch
            {
                ViewBag.Message = "Fout";
                return View(new ClubWeergevenVM());
            }            
        }

        // GET: Clubs/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                //gekozen club weergeven
                Club club = clubsService.ClubWeergeven(id);

                //viewmodel aanmaken met club en adres
                ClubWeergevenVM clubWeergevenVM = new ClubWeergevenVM()
                {
                    Club = club,
                    Adres = adressenService.AdresWeergeven(club.AdresId)
                };

                //edit pagina weergeven met viewmodel
                return View(clubWeergevenVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // POST: Clubs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClubWeergevenVM clubWeergevenVM, HttpPostedFileBase file)
        {
            try
            {
                //methode aanspreken om adresgegevens te wijzigen
                businessServices.AdresAanmakenMetLatLng(clubWeergevenVM.Adres, clubWeergevenVM.Club);

                //club en adres op actief zetten
                clubWeergevenVM.Club.Actief = true;
                clubWeergevenVM.Adres.Actief = true;

                //methodes aanspreken om club en adres te wijzigen
                clubsService.ClubWijzigen(clubWeergevenVM.Club);
                adressenService.AdresWijzigen(clubWeergevenVM.Adres);

                //terugsturen naar de index methode
                return RedirectToAction("Index");
            }
            //indien er iets misloopt wordt de edit pagina opnieuw weergegeven met de viewmodel
            catch
            {
                ViewBag.Message = "Fout";
                return View(clubWeergevenVM);
            }                
        }

        [HttpGet]
        public ActionResult ClubDeactiveren(int id)
        {
            try
            {
                //methode aanspreken om de gekozen club weer te geven
                Club club = clubsService.ClubWeergeven(id);

                //viewmodel aanmaken met club en adres
                ClubWeergevenVM clubWeergevenVM = new ClubWeergevenVM()
                {
                    Club = club,
                    Adres = adressenService.AdresWeergeven(club.AdresId)
                };

                //delete pagina weergeven met de gegevens uit viewmodel
                return View(clubWeergevenVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ClubDeactiveren(ClubWeergevenVM clubWeergevenVM)
        {
            try
            {
                //methodes aanspreken om club en adres weer te geven die we ophalen a.d.h.v. het id dat we halen uit het viewmodel
                Club club = clubsService.ClubWeergeven(clubWeergevenVM.Club.ClubId);
                Adres adres = adressenService.AdresWeergeven(club.AdresId);

                //club en adres deactiveren
                club.Actief = false;
                adres.Actief = false;

                //methode aanspreken om club en adres te wijzigen
                clubsService.ClubWijzigen(club);
                adressenService.AdresWijzigen(adres);

                //terugsturen naar de index methode
                return RedirectToAction("Index");
            }
            //indien er iets misloopt wordt de delete pagina opnieuw weergegeven met het viewmodel
            catch
            {
                ViewBag.Message = "Fout";
                return View(clubWeergevenVM);
            }            
        }
        public ActionResult DownloadKalender(int id)
        {
            //club weergeven a.d.h.v. het meegegeven id
            Club club = clubsService.ClubWeergeven(id);

            //stream openen die automatisch gaat sluiten om de kalender-file toe te voegen aan een zipfile 
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(club.Kalender);
                zip.Save(Server.MapPath("~/myZipFile.zip"));
            }

            //zet het type van de inhoud van de file op application/zip
            Response.ContentType = "application/zip";

            //voegt een header toe 
            Response.AppendHeader("Content-disposition", $"attachment;filename=\"{club.Naam} kalender\"");

            //schrijft de bepaalde file direct naar een http response stream
            Response.TransmitFile(Server.MapPath("~/myZipFile.zip"));

            //de webserver stopt het processen van het script en geeft het resultaat weer
            Response.End();

            //terugsturen naar de details methode
            return RedirectToAction("Details");
        }
    }
}
