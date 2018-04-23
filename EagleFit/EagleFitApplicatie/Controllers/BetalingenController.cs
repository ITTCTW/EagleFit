using DAL.Services;
using EL.Models;
using EL.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EagleFitApplicatie.Controllers
{
    public class BetalingenController : Controller
    {
        private BetalingenService betalingenService = new BetalingenService();
        private LedenService ledenService = new LedenService();
        private PersonenService personenService = new PersonenService();
        private AbonnementenService abonnementenService = new AbonnementenService();
        // GET: Betalingen
        public ActionResult Index()
        {
            try
            {
                //de index pagina met een lijst van alle betalingen weergeven
                return View(betalingenService.AlleBetalingenWeergeven());
            }
            //indien dit niet lukt worden we naar de error pagina gebracht
            catch
            {
                return View("Error");
            }
        }

        // GET: Betalingen/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                //de gekozen betaling weergeven
                Betaling betaling = betalingenService.BetalingWeergeven(id);

                //het lid weergeven die bij de betaling hoort en aan de persoon propertie de persoon toevoegen
                Lid lid = ledenService.LidWeergeven(betaling.Lidnummer);
                lid.Persoon = personenService.PersoonWeergeven(lid.PersoonId);

                //viewmodel aanmaken met de betaling en het lid
                BetalingMetLidVM betalingMetLidVM = new BetalingMetLidVM()
                {
                    Betaling = betaling,
                    Lid = lid
                };

                //de details pagina weergeven met de gegevens die we meegeven in het viewmodel
                return View(betalingMetLidVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // GET: Betalingen/Create
        public ActionResult Create()
        {
            try
            {
                //lijst aanmaken van alle actieve leden die in de database zitten
                List<Lid> alleLeden = ledenService.AlleLedenWeergeven();

                //lege lijst aanmaken voor personen
                List<Persoon> allePersonen = new List<Persoon>();

                //voor elk actief lid gaan we de persoon ervan aan de lijst met personen toevoegen
                foreach (Lid lid in alleLeden)
                {
                    allePersonen.Add(personenService.PersoonWeergeven(lid.PersoonId));
                }

                //viewmodel aanmaken met alle leden in om in de view aan een dropdownlist toe te voegen
                BetalingAanmakenVM betalingAanmakenVM = new BetalingAanmakenVM()
                {
                    AlleLeden = allePersonen
                };

                //create pagina weergeven met de gegevens in het viewmodel
                return View(betalingAanmakenVM);
            }
            //indien er iets misloopt wordt de error pagina weergeven
            catch
            {
                return View("Error");
            }
        }

        // POST: Betalingen/Create
        [HttpPost]
        public ActionResult Create(BetalingAanmakenVM betalingAanmakenVM)
        {
            try
            {
                //lid ophalen uit viewmodel
                Lid lid = ledenService.LidWeergeven(ledenService.LidnummerMetPersoonsIdWeergeven(betalingAanmakenVM.PersoonsId));

                //abonnement ophalen a.d.h.v. het abonnementid van het lid
                Abonnement abonnement = abonnementenService.AbonnementWeergeven(lid.AbonnementId);  
                
                //betaling ophalen uit viewmodel en bepaalde propperties hun waarde geven          
                Betaling betaling = betalingAanmakenVM.Betaling;
                betaling.Lidnummer = lid.LidNummer;
                betaling.Bedrag = abonnement.PrijsPerMaand;
                betalingenService.BetalingToevoegen(betaling);

                //terugsturen naar de index methode
                return RedirectToAction("Index");
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                ViewBag.Message = "Fout";
                return View();
            }
        }

        // GET: Betalingen/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                //lijst aanmaken van alle actieve leden die in de database zitten
                List<Lid> alleLeden = ledenService.AlleLedenWeergeven();

                //lege lijst aanmaken voor personen
                List<Persoon> allePersonen = new List<Persoon>();

                //voor elk actief lid gaan we de persoon ervan aan de lijst met personen toevoegen
                foreach (Lid item in alleLeden)
                {
                    allePersonen.Add(personenService.PersoonWeergeven(item.PersoonId));
                }

                //lid van de betaling weergeven
                Lid lid = ledenService.LidWeergeven(betalingenService.BetalingWeergeven(id).Lidnummer);

                //viewmodel aanmaken met alle leden in om in de view aan een dropdownlist toe te voegen
                BetalingAanmakenVM betalingAanmakenVM = new BetalingAanmakenVM()
                {
                    Betaling = betalingenService.BetalingWeergeven(id),
                    AlleLeden = allePersonen,
                    PersoonsId = lid.PersoonId
                };

                //edit pagina weergeven met de gegevens in het viewmodel
                return View(betalingAanmakenVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }

        }

        // POST: Betalingen/Edit/5
        [HttpPost]
        public ActionResult Edit(BetalingAanmakenVM betalingAanmakenVM)
        {
            try
            {
                //lid ophalen uit viewmodel
                Lid lid = ledenService.LidWeergeven(ledenService.LidnummerMetPersoonsIdWeergeven(betalingAanmakenVM.PersoonsId));

                //abonnement ophalen a.d.h.v. het abonnementid van het lid
                Abonnement abonnement = abonnementenService.AbonnementWeergeven(lid.AbonnementId);

                //betalingsgegevens aanpassen a.d.h.v. de meegegeven gegevens in viewmodel
                betalingAanmakenVM.Betaling.Bedrag = abonnement.PrijsPerMaand;
                betalingAanmakenVM.Betaling.Lidnummer = lid.LidNummer;

                //methode aanspreken om de betaling te wijzigen
                betalingenService.BetalingWijzigen(betalingAanmakenVM.Betaling);

                //terugsturen naar de index methode
                return RedirectToAction("Index");
            }
            //indien er iets misloopt wordt de edit pagina opnieuw weergegeven met de viewmodel
            catch
            {
                ViewBag.Message = "Fout";
                return View(betalingAanmakenVM);
            }
        }

        // GET: Betalingen/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                //de delete pagina weergeven met de betaling
                return View(betalingenService.BetalingWeergeven(id));
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // POST: Betalingen/Delete/5
        [HttpPost]
        public ActionResult Delete(Betaling betaling)
        {
            try
            {
                //de methode oproepen om de betaling te verwijderen
                betalingenService.BetalingVerwijderen(betaling.BetalingsId);

                //terugsturen naar de index methode
                return RedirectToAction("Index");
            }
            //indien er iets misloopt wordt de delete pagina opnieuw weergegeven met de betaling
            catch
            {
                ViewBag.Message = "Fout";
                return View(betaling);
            }
        }
    }
}
