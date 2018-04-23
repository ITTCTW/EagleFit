using DAL.Services;
using EL.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EagleFitApplicatie.Controllers
{
    public class MedewerkersController : Controller
    {
        private MedewerkersService medewerkersService = new MedewerkersService();
        private AdressenService adressenService = new AdressenService();
        private PersonenService personenService = new PersonenService();
        private UserService userService = new UserService();
        // GET: Medewerkers
        public ActionResult Index()
        {
            try
            {
                //lijst aanmaken met alle actieve medewerkers
                List<Medewerker> medewerkers = medewerkersService.AlleActieveMedewerkersWeergeven();

                //voor elke medewerker in de medewerkerslijst de persoon en zijn adres aan de properties toevoegen a.d.h.v. het id (FK)
                foreach (var item in medewerkers)
                {
                    item.Persoon = personenService.PersoonWeergeven(item.PersoonsId);
                    item.Persoon.Adres = adressenService.AdresWeergeven(item.Persoon.AdresId);
                }

                //index pagina weergeven met medewerkerslijst
                return View(medewerkers);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // GET: Medewerkers/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                //de gekozen medewerker weergeven
                Medewerker medewerker = medewerkersService.MedewerkerWeergeven(id);
                medewerker.Persoon = personenService.PersoonWeergeven(medewerker.PersoonsId);
                medewerker.Persoon.Adres = adressenService.AdresWeergeven(medewerker.Persoon.AdresId);

                //de details pagina weergeven met de medewerker
                return View(medewerker);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // GET: Medewerkers/Create
        public ActionResult Create()
        {
            try
            {
                //de create pagina weergeven
                return View();
            }            
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // POST: Medewerkers/Create
        [HttpPost]
        public ActionResult Create(Medewerker medewerker)
        {
            try
            {
                //medewerker actief zetten en toevoegen aan de database, de andere properties heeft hij gekregen in het formulier van de create pagina
                medewerker.Actief = true;
                medewerkersService.MedewerkerToevoegen(medewerker);

                //terugsturen naar de index methode       
                return RedirectToAction("Index");
            }
            //indien er iets misloopt wordt de create pagina opnieuw weergegeven
            catch
            {
                ViewBag.Message = "Fout";
                return View();
            }
        }

        // GET: Medewerkers/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                //de gekozen medewerker weergeven
                Medewerker medewerker = medewerkersService.MedewerkerWeergeven(id);
                medewerker.Persoon = personenService.PersoonWeergeven(medewerker.PersoonsId);
                medewerker.Persoon.Adres = adressenService.AdresWeergeven(medewerker.Persoon.AdresId);

                //de edit pagina weergeven met de medewerker
                return View(medewerker);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // POST: Medewerkers/Edit/5
        [HttpPost]
        public ActionResult Edit(Medewerker medewerker)
        {
            try
            {
                //de al opgeslagen medewerker weergeven
                Medewerker oudeMedewerker = medewerkersService.MedewerkerWeergeven(medewerker.MedewerkersId);

                //een nieuwe medewerker aanmaken die de properties krijgt van het zojuist ingevulde formulier
                Medewerker nieuweMedewerker = medewerker;
                nieuweMedewerker.Persoon = medewerker.Persoon;
                nieuweMedewerker.PersoonsId = medewerker.Persoon.PersoonsId;
                nieuweMedewerker.Persoon.Adres = medewerker.Persoon.Adres;
                nieuweMedewerker.Persoon.Adres.AdresId = medewerker.Persoon.AdresId;
                nieuweMedewerker.Actief = true;
                
                //medewerker, persoon en adres in de database wijzigen
                medewerkersService.MedewerkerWijzigen(nieuweMedewerker);
                personenService.PersoonWijzigen(nieuweMedewerker.Persoon);
                adressenService.AdresWijzigen(nieuweMedewerker.Persoon.Adres);

                //role aan user toevoegen en oude user role verwijderen
                userService.UserRoleWijzigen(nieuweMedewerker.PersoonsId, nieuweMedewerker.MedewerkersStatus);
                userService.UserRoleVerwijderen(oudeMedewerker.PersoonsId, oudeMedewerker.MedewerkersStatus);

                //terugsturen naar de index methode
                return RedirectToAction("Index");                
            }
            //indien er iets misloopt wordt de edit pagina opnieuw weergegeven met de medewerker
            catch
            {
                ViewBag.Message = "Fout";
                return View(medewerker);
            }
        }

        [HttpGet]
        public ActionResult MedewerkerDeactiveren(int id)
        {
            try
            {
                //de gekozen medewerker en de bijhorende persoon weergeven
                Medewerker medewerker = medewerkersService.MedewerkerWeergeven(id);
                medewerker.Persoon = personenService.PersoonWeergeven(medewerker.PersoonsId);

                //de delete pagina weergeven met de medewerker
                return View(medewerker);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult MedewerkerDeactiveren(Medewerker medewerker)
        {
            try
            {
                //de medewerker weergeven en actief op false zetten
                medewerker = medewerkersService.MedewerkerWeergeven(medewerker.MedewerkersId);
                medewerker.Actief = false;

                //de medewerker wijzigen
                medewerkersService.MedewerkerWijzigen(medewerker);

                //terugsturen naar de index methode
                return RedirectToAction("Index");
            }
            //indien er iets misloopt wordt de delete pagina opnieuw weergegeven met de medewerker
            catch
            {
                ViewBag.Message = "Fout";
                return View(medewerker);
            }            
        }
    }
}
