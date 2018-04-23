using System.Web.Mvc;
using EL.Models;
using DAL.Services;

namespace EagleFitApplicatie.Controllers
{
    public class AbonnementenController : Controller
    {
        private AbonnementenService abonnementService = new AbonnementenService();

        // GET: Abonnementen
        public ActionResult Index()
        {
            try
            {
                //lijst van abonnementen in de view weergeven
                return View(abonnementService.AlleAbonnementenWeergeven());
            }

            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // GET: Abonnementen/Details/1
        public ActionResult Details(int? id)
        {
            try
            {
                //details pagina weergeven met gekozen abonnement
                return View(abonnementService.AbonnementWeergeven(id));
            }

            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }

        }

        // GET: Abonnementen/Create
        public ActionResult Create()
        {
            try
            {
                //geeft de create pagina weer
                return View();
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // POST: Abonnementen/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AbonnementId,Naam,Omschrijving,PrijsPerMaand")] Abonnement abonnement)
        {
            try
            {
                //het abonnement op actief zitten
                abonnement.Actief = true;

                //methode aanspreken om abonnement toe te voegen aan de database
                abonnementService.AbonnementToevoegen(abonnement);

                //terugsturen naar de index methode
                return RedirectToAction("Index");
            }
            //indien dit misloopt krijgen we opnieuw de create pagina
            catch
            {
                ViewBag.Message = "Fout";
                return View(abonnement);
            }
        }

        // GET: Abonnementen/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                //de edit pagina weergeven met gekozen abonnement
                return View(abonnementService.AbonnementWeergeven(id));
            }
            //indien er iets misgaat worden we naar de error pagina gebracht
            catch
            {
                return View("Error");
            }

        }

        // POST: Abonnementen/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AbonnementId,Naam,Omschrijving,PrijsPerMaand")] Abonnement abonnement)
        {
            try
            {
                //abonnement op actief zetten
                abonnement.Actief = true;

                //methode aanspreken om abonnement te wijzigen
                abonnementService.AbonnementWijzigen(abonnement);

                //terugsturen naar index methode
                return RedirectToAction("Index");
            }
            //indien er iets misloopt krijgen we opnieuw de edit pagina met ons abonnement
            catch
            {
                ViewBag.Message = "Fout";
                return View(abonnement);
            }


        }

        [HttpGet]
        public ActionResult AbonnementDeactiveren(int id)
        {
            try
            {
                //de delete pagina weergeven met het gekozen abonnement
                return View(abonnementService.AbonnementWeergeven(id));
            }
            //indien er iets misgaat worden we naar de error pagina gebracht
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult AbonnementDeactiveren(Abonnement abonnement)
        {
            try
            {
                //abonnement weergeven
                abonnement = abonnementService.AbonnementWeergeven(abonnement.AbonnementId);
                
                //abonnement deactiveren
                abonnement.Actief = false;

                //methode aanspreken om abonnement te wijzigen
                abonnementService.AbonnementWijzigen(abonnement);
                return RedirectToAction("Index");
            }

            //indien er iets misgaat krijgen we opnieuw de delete view met ons abonnement
            catch
            {
                ViewBag.Message = "Fout";
                return View(abonnementService.AbonnementWeergeven(abonnement.AbonnementId));
            }
        }
    }
}
