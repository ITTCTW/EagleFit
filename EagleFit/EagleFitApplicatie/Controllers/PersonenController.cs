using DAL.Services;
using EL.Models;
using EL.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Web;
using System.Web.Mvc;
using static EagleFitApplicatie.App_Start.IdentityConfig;

namespace EagleFitApplicatie.Controllers
{
    public class PersonenController : Controller
    {
        private PersonenService personenService = new PersonenService();
        private AbonnementenService abonnementenService = new AbonnementenService();
        private BetalingenService betalingenService = new BetalingenService();
        private LedenService ledenService = new LedenService();
        private AdressenService adressenService = new AdressenService();
        private ApplicationUserManager _userManager;

        public PersonenController()
        {

        }
        public PersonenController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Personen
        public ActionResult Index()
        {
            try
            {
                //de index pagina weergeven
                return View();
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }
        public ActionResult Persoonsgegevens()
        {
            try
            {
                //de user ophalen
                string userId = User.Identity.GetUserId();

                //persoon met adres weergeven
                Persoon persoon = personenService.PersoonWeergeven(userId);
                persoon.Adres = adressenService.AdresWeergeven(persoon.AdresId);

                //viewmodel aanmaken met persoon en lidnummer
                PersoonDetailsVM persoonDetailsVM = new PersoonDetailsVM()
                {
                    Persoon = persoon,
                    Lidnummer = ledenService.LidnummerMetPersoonsIdWeergeven(persoon.PersoonsId)
                };

                //persoonsgegevens pagina weergeven met viewmodel
                return View(persoonDetailsVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }
        public ActionResult Abonnementsgegevens()
        {
            try
            {
                //userid ophalen
                string userId = User.Identity.GetUserId();

                //lid weergeven
                Lid lid = ledenService.LidWeergeven(userId);
                lid.Persoon = personenService.PersoonWeergeven(lid.PersoonId);

                //viewmodel aanmaken met lid en abonnement
                AbonnementVanLidWeergevenVM abonnementVanLidVM = new AbonnementVanLidWeergevenVM()
                {
                    Lid = lid,
                    Abonnement = abonnementenService.AbonnementWeergeven(lid.AbonnementId)
                };

                //abonnementspagina weergeven met viewmodel
                return View(abonnementVanLidVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }
        public ActionResult BetalingenPerLid()
        {
            try
            {
                //userid ophalen
                string userId = User.Identity.GetUserId();

                //methode aanspreken om alle betalingen van het lid weer te geven
                return View(betalingenService.AlleBetalingenVanLid(userId));
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult BetalingenPerLid(FormCollection form)
        {
            try
            {
                //betaling opvragen
                ValueProviderResult betalingOphalen = form.GetValue("item.BetalingsId");
                Betaling betaling = betalingenService.BetalingWeergeven(Convert.ToInt32(betalingOphalen.AttemptedValue));

                //betaling als betaald zetten
                betaling.Betaald = true;

                //betaling wijzigen
                betalingenService.BetalingWijzigen(betaling);

                //pagina weergeven van betalingen van de user
                string userId = User.Identity.GetUserId();
                return View(betalingenService.AlleBetalingenVanLid(userId));
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult WijzigPersoonsgegevens(int id)
        {
            try
            {
                //persoon en het adres weergeven
                Persoon persoon = personenService.PersoonWeergeven(id);
                persoon.Adres = adressenService.AdresWeergeven(persoon.AdresId);

                //de pagina weergeven met de persoon
                return View(persoon);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult WijzigPersoonsgegevens(Persoon persoon)
        {
            try
            {
                //adres en persoon wijzigen
                adressenService.AdresWijzigen(persoon.Adres);
                personenService.PersoonWijzigen(persoon);

                //terugsturen naar de persoonsgegevens pagina
                return RedirectToAction("Persoonsgegevens");
            }
            //indien er iets misloopt wordt de pagina opnieuw weergegeven met de persoon
            catch
            {
                ViewBag.Message = "Fout";
                return View(persoon);
            }
        }
        [HttpGet]
        public ActionResult WijzigAbonnementsgegevens(int id)
        {
            try
            {
                //lid weergeven
                Lid lid = ledenService.LidWeergeven(id);
                lid.Persoon = personenService.PersoonWeergeven(lid.PersoonId);

                //viewmodel aanmaken met lid en alle abonnementen
                LidMetAbonnementenVM lidMetAbonnementenVM = new LidMetAbonnementenVM()
                {
                    Lid = lid,
                    AlleAbonnementen = abonnementenService.AlleAbonnementenWeergeven()
                };

                //de pagina weergeven met viewmodel
                return View(lidMetAbonnementenVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult WijzigAbonnementsgegevens(LidMetAbonnementenVM lidMetAbonnementenVM)
        {
            try
            {
                //lid weergeven en het tewijzigenabonnement toekennen, deze wordt dan bij het starten van het programma in het begin van de maand gewijzigd, niet rechtstreeks
                Lid lid = ledenService.LidWeergeven(lidMetAbonnementenVM.Lid.LidNummer);
                lid.TeWijzigenAbonnementId = lidMetAbonnementenVM.Lid.AbonnementId;

                //methode aanspreken om het lid te wijzigen
                ledenService.LidWijzigen(lid);

                //terugsturen naar abonnementsgegevenspagina
                return RedirectToAction("Abonnementsgegevens");
            }
            //indien er iets misloopt wordt de pagina opnieuw weergegeven met het viewmodel
            catch
            {
                ViewBag.Message = "Fout";
                return View(lidMetAbonnementenVM);
            }
        }
    }
}