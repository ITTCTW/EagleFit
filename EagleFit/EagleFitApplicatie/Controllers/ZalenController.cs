using DAL.Services;
using EL.Models;
using EL.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EagleFitApplicatie.Controllers
{
    public class ZalenController : Controller
    {
        private ZalenService zalenService = new ZalenService();
        private ClubsService clubsService = new ClubsService();
        // GET: Zalen
        public ActionResult Index()
        {
            try
            {
                //lijst met alle zalen
                List<Zaal> zalen = zalenService.AlleZalenWeergeven();

                //voor elke lijst de club aan de propertie toekennen a.d.h.v. de FK
                foreach (var item in zalen)
                {
                    item.Club = clubsService.ClubWeergeven(item.ClubId);
                }

                //de index pagina weergeven met de lijst van zalen
                return View(zalen);
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Zalen/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                //de gekozen zaal met club weergeven
                Zaal zaal = zalenService.ZaalWeergeven(id);
                zaal.Club = clubsService.ClubWeergeven(zaal.ClubId);

                //de details pagina weergeven met de zaal
                return View(zaal);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // GET: Zalen/Create
        public ActionResult Create()
        {
            try
            {
                //viewmodel aanmaken met alle clubs
                ZaalAanmakenVM zaalAanmakenVM = new ZaalAanmakenVM()
                {
                    AlleClubs = clubsService.AlleClubsWeergeven()
                };

                //create pagina weergeven met viewmodel
                return View(zaalAanmakenVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // POST: Zalen/Create
        [HttpPost]
        public ActionResult Create(ZaalAanmakenVM zaalAanmakenVM)
        {
            try
            {
                //de zaal ophalen en op actief zetten
                Zaal zaal = zaalAanmakenVM.Zaal;
                zaal.Actief = true;

                //de zaal toevoegen aan de database
                zalenService.ZaalToevoegen(zaal);

                //terugsturen naar de index pagina
                return RedirectToAction("Index");
            }
            //indien er iets misloopt de create pagina opnieuw weergeven
            catch
            {
                ViewBag.Message = "Fout";
                return View(zaalAanmakenVM);
            }
        }

        // GET: Zalen/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                //viewmodel aanmaken met alle clubs en de zaal
                ZaalAanmakenVM zaalAanmakenVM = new ZaalAanmakenVM()
                {
                    AlleClubs = clubsService.AlleClubsWeergeven(),
                    Zaal = zalenService.ZaalWeergeven(id)
                };

                //de edit pagina weergeven met het viewmodel
                return View(zaalAanmakenVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // POST: Zalen/Edit/5
        [HttpPost]
        public ActionResult Edit(ZaalAanmakenVM zaalAanmakenVM)
        {
            try
            {
                //de zaal ophalen uit het viewmodel en op actief zetten
                Zaal zaal = zaalAanmakenVM.Zaal;
                zaal.Actief = true;

                //de methode oproepen om de zaal te wijzigen
                zalenService.ZaalWijzigen(zaal);

                //terugsturen naar de index pagina
                return RedirectToAction("Index");
            }
            //indien er iets misloopt wordt de edit pagina opnieuw weergegeven met de zaal
            catch
            {
                ViewBag.Message = "Fout";
                return View(zalenService.ZaalWeergeven(zaalAanmakenVM.Zaal.ZaalId));
            }
        }

        [HttpGet]
        public ActionResult ZaalDeactiveren(int id)
        {
            try
            {
                //de zaal weergeven met de bijhorende club a.d.h.v. de keys
                Zaal zaal = zalenService.ZaalWeergeven(id);
                zaal.Club = clubsService.ClubWeergeven(zaal.ClubId);

                //de delete pagina weergeven met de zaal
                return View(zaal);
            }
            //indien er iets misloopt de error pagina weergeven
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ZaalDeactiveren(Zaal zaal)
        {
            try
            {
                //de zaal ophalen uit de database a.d.h.v. het id en de zaal deactiveren door actief op false te zetten
                zaal = zalenService.ZaalWeergeven(zaal.ZaalId);
                zaal.Actief = false;

                //de methode opvragen om de zaal te wijzigen
                zalenService.ZaalWijzigen(zaal);

                //terugsturen naar de index pagina
                return RedirectToAction("Index");
            }
            //indien er iets misloopt wordt de delete pagina opnieuw weergegeven met de zaal
            catch
            {
                ViewBag.Message = "Fout";
                return View(zaal);
            }
        }
    }
}
