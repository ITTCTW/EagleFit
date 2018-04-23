using System.Web;
using System.Web.Mvc;
using EL.Models;
using DAL.Services;
using EL.ViewModels;
using EL.BLServices;

namespace EagleFitApplicatie.Controllers
{
    public class AdressenController : Controller
    {
        private AdressenService adressenService = new AdressenService();
        private ClubsService clubsService = new ClubsService();
        private BusinessServices businessServices = new BusinessServices();

        // GET: Adressen
        public ActionResult Index()
        {
            try
            {
                //de index pagina met een lijst van alle adressen weergeven
                return View(adressenService.AlleAdressenWeergeven());
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // GET: Adressen/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                //het gekozen adres weergeven
                Adres adres = adressenService.AdresWeergeven(id);

                //viewmodel aanmaken met adres en club
                ClubWeergevenVM adresMetClubWeergevenVM = new ClubWeergevenVM()
                {
                    Adres = adres,
                    Club = clubsService.ClubWeergeven(adres.ClubId)
                };

                //de details pagina weergeven met de gegevens in de viewmodel
                return View(adresMetClubWeergevenVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
            
        }

        // GET: Adressen/Create
        public ActionResult Create()
        {
            try
            {
                //viewmodel aanmaken met alle clubs om aan een dropdownlist toe te voegen
                AdresMetClubsVM adresMetClubsVM = new AdresMetClubsVM()
                {
                    AlleClubs = clubsService.AlleClubsWeergeven()
                };

                //create pagina weergeven met het viewmodel
                return View(adresMetClubsVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // POST: Adressen/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdresMetClubsVM adresMetClubsVM, HttpPostedFileBase file)
        {                                  
            try
            {
                //adres en club ophalen uit viewmodel
                Adres adres = adresMetClubsVM.Adres;
                Club club = adresMetClubsVM.Club;

                //methode aanspreken om adres aan te maken met alle gegevens
                businessServices.AdresAanmakenMetLatLng(adres, club);

                //indien de checkbox aangeduid is club aanmaken
                if (adresMetClubsVM.chkbxClub == true)
                {
                    businessServices.ClubAanmaken(club, adres, file);
                    adres.ClubId = club.ClubId;
                }
                //is checkbox niet aangeduid dan wordt de clubid op 0 gezet
                else
                    adres.ClubId = 0;

                //adres op actief zetten
                adres.Actief = true;

                //methode aanspreken om adres aan de database toe te voegen
                adressenService.AdresToevoegen(adres);

                //terugsturen naar de index methode
                return RedirectToAction("Index");
            }
            //indien er iets misloopt wordt de create pagina opnieuw weergegeven
            catch
            {
                ViewBag.Message = "Fout";
                return View(adresMetClubsVM);
            }            
        }
        

        // GET: Adressen/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            bool heeftClub;
            try
            {
                //adres weergeven
                Adres adres = adressenService.AdresWeergeven(id);
                
                //indien adres een club geeft "heeftClub" op true zetten
                if (adres.ClubId == 0)
                    heeftClub = false;
                else
                    heeftClub = true;
                
                //viewmodel aanmaken
                AdresMetClubsVM adresMetClubsVM = new AdresMetClubsVM
                {
                    Adres = adres,
                    AlleClubs = clubsService.AlleClubsWeergeven(),
                    chkbxClub = heeftClub
                };

                //edit pagina weergeven met viewmodel
                return View(adresMetClubsVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // POST: Adressen/Edit/5
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(AdresMetClubsVM adresMetClubsVM)
        {
            try
            {
                //clubid waarde geven naargelang checkbox was aangeduid of niet
                if (adresMetClubsVM.chkbxClub == true)
                    adresMetClubsVM.Adres.ClubId = adresMetClubsVM.Adres.ClubId;
                else
                    adresMetClubsVM.Adres.ClubId = 0;

                //club weergeven
                Club club = clubsService.ClubWeergeven(adresMetClubsVM.Adres.ClubId);
                
                //indien er een clubid is, methode aanspreken om alle gegevens aan te vullen
                if (club.ClubId != 0)
                {
                    businessServices.AdresAanmakenMetLatLng(adresMetClubsVM.Adres, club);
                }

                //adres actief zetten
                adresMetClubsVM.Adres.Actief = true;

                //methode aanspreken om het adres te wijzigen
                adressenService.AdresWijzigen(adresMetClubsVM.Adres);

                //terugsturen naar de index methode
                return RedirectToAction("Index");
            }
            //indien er iets misloopt wordt de edit pagina opnieuw weergegeven met de viewmodel
            catch
            {
                ViewBag.Message = "Fout";
                return View(adresMetClubsVM);
            }
        }

        [HttpGet]
        public ActionResult AdresDeactiveren(int id)
        {
            try
            {
                //de delete pagina weergeven met het adres
                return View(adressenService.AdresWeergeven(id));
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult AdresDeactiveren(Adres adres)
        {
            try
            {
                //het adres weergeven, deactiveren en de methode oproepen om dit adres te wijzigen
                adres = adressenService.AdresWeergeven(adres.AdresId);
                adres.Actief = false;
                adressenService.AdresWijzigen(adres);

                //terugsturen naar de index methode
                return RedirectToAction("Index");
            }
            //indien er iets misloopt wordt de delete pagina opnieuw weergegeven met het adres
            catch
            {
                ViewBag.Message = "Fout";
                return View(adressenService.AdresWeergeven(adres.AdresId));
            }            
        }
    }
}
