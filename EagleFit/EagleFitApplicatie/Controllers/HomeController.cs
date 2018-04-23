using DAL.Services;
using System.Collections.Generic;
using System.Web.Mvc;
using EL.Models;

namespace EagleFitApplicatie.Controllers
{
    public class HomeController : Controller
    {
        private AbonnementenService abonnementenService = new AbonnementenService();
        private AdressenService adressenService = new AdressenService();
        private ClubsService clubsService = new ClubsService();
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
        [HttpGet]
        public ActionResult Clubs()
        {
            try
            {
                //de clubs pagina met de adressen van alle clubs weergeven
                return View(adressenService.AlleAdressenClubs());
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }
        //[HttpPost]
        //public ActionResult Clubs(string Location)
        //{
        //    try
        //    {
        //        //de clubs pagina weergeven
        //        return View();
        //    }
        //    //indien er iets misloopt wordt de error pagina weergegeven
        //    catch
        //    {
        //        return View("Error");
        //    }
        //}
        public ActionResult Over()
        {
            try
            {
                //de over pagina weergeven
                return View();
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }
        public ActionResult Groepslessen()
        {
            try
            {
                //de groepslessen pagina weergeven
                return View();
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }
        public ActionResult Prijzen()
        {
            try
            {
                //de prijzen pagina weergeven met alle abonnementen
                return View(abonnementenService.AlleAbonnementenWeergeven());
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }
        public ActionResult Contact()
        {
            try
            {
                //de contact pagina weergeven
                return View();
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }
        [HttpGet]
        public JsonResult GetData()
        {
            try
            {
                //lijst met alle clubs weergeven
                List<Club> ClubsAdressen = clubsService.AlleClubsWeergeven();

                //lijst met adressen weergeven waar nog geen adressen inzitten
                List<Adres> adressen = new List<Adres>();

                //voor elke actieve club in de database het adres toevoegen aan de lijst van adressen
                foreach (var item in ClubsAdressen)
                {
                    adressen.Add(adressenService.AdresWeergeven(item.AdresId));
                }

                //de adressen teruggeven aan de clubs pagina om de google maps te tonen met een marker op alle adressen van de clubs
                return Json(adressen, JsonRequestBehavior.AllowGet);
            }
            //indien er iets misloopt wordt het Json resultaat op "null" gezet en gaan we dit valideren in de clubs view
            catch
            {
                return Json(null);
            }
        }
        public ActionResult Admin()
        {
            try
            {
                //admin pagina weergeven
                return View();
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }
    }
}