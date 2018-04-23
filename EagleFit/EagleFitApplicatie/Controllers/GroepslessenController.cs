using System.Net;
using System.Web.Mvc;
using EL.Models;
using EL.ViewModels;
using DAL.Services;
using static EagleFitApplicatie.App_Start.IdentityConfig;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace EagleFitApplicatie.Controllers
{
    public class GroepslessenController : Controller
    {
        private GroepslessenService groepslessenService = new GroepslessenService();
        private ClubsService clubsService = new ClubsService();
        private PersonenService personenService = new PersonenService();
        private LedenService ledenService = new LedenService();
        private ApplicationUserManager _userManager;

        public GroepslessenController()
        {

        }
        public GroepslessenController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return UserManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Groepslessen
        public ActionResult Index()
        {
            try
            {
                //de index pagina met een lijst van alle groepslessen weergeven
                return View(groepslessenService.AlleGroepslessenWeergeven());
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // GET: Groepslessen/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                //de gekozen groepsles weergeven
                Groepsles groepsles = groepslessenService.GroepslesWeergeven(id);

                //de details pagina weergeven met de gegevens in het viewmodel
                return View(groepsles);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // GET: Groepslessen/Create
        public ActionResult Create()
        {
            try
            {
                //create pagina weergeven
                return View();
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                ViewBag.Message = "Fout";
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroepslesId,naam,beschrijving")] Groepsles groepsles)
        {
            try
            {
                //groepsles op actief zetten, de andere properties komen van de view en de velden die de gebruiker ingevuld heeft
                groepsles.Actief = true;

                //groepsles toevoegen aan de database
                groepslessenService.GroepslesToevoegen(groepsles);

                //terugsturen naar de index methode
                return RedirectToAction("Index");  
            }
            //indien er iets misloopt wordt de create pagina opnieuw weergegeven
            catch
            {
                return View(groepsles);
            }                
        }

        // GET: Groepslessen/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                //methode aanspreken om gekozen groepsles weer te geven
                Groepsles groepsles = groepslessenService.GroepslesWeergeven(id);

                //edit pagina weergeven met gekozen groepsles
                return View(groepsles);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // POST: Groepslessen/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroepslesId,naam,beschrijving")] Groepsles groepsles)
        {
            try
            {
                //groepsles op actief zetten
                groepsles.Actief = true;

                //groepsles wijzigen
                groepslessenService.groepslesWijzigen(groepsles);

                //terugsturen naar de index methode
                return RedirectToAction("Index");
            }
            //indien er iets misloopt wordt de edit pagina opnieuw weergegeven met de groepsles
            catch
            {
                ViewBag.Message = "Fout";
                return View(groepsles);
            }           
        }

        [HttpGet]
        public ActionResult GroepslesDeactiveren(int id)
        {
            try
            {
                //de delete pagina weergeven met de groepsles
                return View(groepslessenService.GroepslesWeergeven(id));
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult GroepslesDeactiveren(Groepsles groepsles)
        {
            try
            {
                //groepsles weergeven, deactiveren en de methode oproepen om de groepsles te wijzigen
                groepsles = groepslessenService.GroepslesWeergeven(groepsles.GroepslesId);
                groepsles.Actief = false;
                groepslessenService.groepslesWijzigen(groepsles);

                //terugsturen naar de index methode
                return RedirectToAction("Index");
            }
            //indien er iets misloopt wordt de delete pagina opnieuw weergegeven met de groepsles
            catch
            {
                ViewBag.Message = "Fout";
                return View(groepslessenService.GroepslesWeergeven(groepsles.GroepslesId));
            }
            
        }
        [HttpGet]
        public ActionResult GroepslessenInClub()
        {
            try
            {
                if (User.IsInRole("Lid"))
                {
                    Persoon persoon = personenService.PersoonWeergeven(User.Identity.GetUserId());
                    Lid lid = ledenService.LidWeergeven(ledenService.LidnummerMetPersoonsIdWeergeven(persoon.PersoonsId));
                    Club club = clubsService.ClubWeergeven(lid.ClubId);
                    GroepslessenPerClubVM GroepslessenPerClubVM = new GroepslessenPerClubVM()
                    {
                        Club = club,
                        Groepslessen = groepslessenService.GroepslessenPerClubWeergeven(club.ClubId)
                    };
                    return View("GroepslessenPerClub", GroepslessenPerClubVM);
                }
                else
                {
                    ClubsVM clubsVM = new ClubsVM()
                    {
                        Clubs = clubsService.AlleClubsWeergeven()
                    };
                    return View(clubsVM);
                }
                
                
            }
            catch
            {
                return View("Error");
            }
            
        }
        [HttpPost]
        public ActionResult GroepslessenInClub(ClubsVM clubsVM)
        {
            //club weergeven die meegegeven is in de viewmodel
            Club club = clubsService.ClubWeergeven(clubsVM.GeselecteerdeClub);

            //viewmodel aanmaken met club en groepslessen
            GroepslessenPerClubVM GroepslessenPerClubVM = new GroepslessenPerClubVM()
            {
                Club = club,
                Groepslessen = groepslessenService.GroepslessenPerClubWeergeven(clubsVM.GeselecteerdeClub)
            };

            //GroepslessenPerClub pagina weergeven met viewmodel die we zojuist aangemaakt hebben
            return View("GroepslessenPerClub", GroepslessenPerClubVM);
        }
    }
}
