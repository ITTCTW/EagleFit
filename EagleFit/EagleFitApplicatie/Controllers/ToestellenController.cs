using DAL.Services;
using EL.Models;
using EL.ViewModels;
using System.Web;
using System.Web.Mvc;
using Ionic.Zip;

namespace EagleFitApplicatie.Controllers
{
    public class ToestellenController : Controller
    {
        private ToestellenService toestellenservice = new ToestellenService();
        

        public ActionResult Index()
        {
            try
            {
                //de index pagina weergeven met een lijst van alle toestellen
                return View(toestellenservice.AlleToestellenWeergeven());
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        
        public ActionResult Details(int id)
        {
            try
            {
                //de details pagina weergeven met het gekozen toestel
                return View(toestellenservice.ToestelWeergeven(id));
            }
            //indien er iets misloopt de error pagina weergeven
            catch
            {
                return View("Error");
            }
        }

        
        public ActionResult Create()
        {
            try
            {
                //de create pagina weergeven
                return View();
            }
            //indien er iets misloopt de error pagina weergeven
            catch
            {
                return View("Error");
            }
        }

        
        [HttpPost]
        public ActionResult Create(Toestel toestel, HttpPostedFileBase file)
        {
            try
            {
                //naam van de file toekennen aan de variabele toekennen
                string filename = file.FileName;

                //path instellen waar en onder welke naam de handleiding opgeslagen gaat worden
                string handleidingPath = Server.MapPath($"~/Handleidingen/{filename}");

                //de file opslaan
                file.SaveAs(handleidingPath);

                //toestel properties een waarde toekennen
                toestel.Handleiding = handleidingPath;
                toestel.Actief = true;

                //het toestel toevoegen aan de database
                toestellenservice.ToestelToevoegen(toestel);

                //terugsturen naar de index pagina
                return RedirectToAction("Index");
            }
            //indien er iets misloopt de create pagina opnieuw weergeven
            catch
            {
                ViewBag.Message = "Fout";
                return View();
            }
        }

        
        public ActionResult Edit(int id)
        {
            try
            {
                //de edit pagina weergeven met het gekozen toestel
                return View(toestellenservice.ToestelWeergeven(id));
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        
        [HttpPost]
        public ActionResult Edit(Toestel toestel, HttpPostedFileBase file)
        {
            try
            {
                //toestel ophalen uit de database a.d.h.v. het id
                Toestel opgehaaldToestel = toestellenservice.ToestelWeergeven(toestel.ToestelId);

                //als de handleiding uit de database niet gelijk is aan de handleiding hier meegegeven door de gebruiker
                if(opgehaaldToestel.Handleiding != toestel.Handleiding)
                {
                    //filename van de file aan deze variabele toekennen
                    string filename = file.FileName;

                    //path instellen waar en onder welke naam de file opgeslaan wordt
                    string handleidingPath = Server.MapPath($"~/Handleidingen/{filename}");

                    //de file opslaan
                    file.SaveAs(handleidingPath);

                    //het path toekennen aan de handleiding propertie van het toestel
                    toestel.Handleiding = handleidingPath;
                }

                //de wijzigingen toepassen op de properties en het toestel op actief zetten
                opgehaaldToestel.ToestelId = toestel.ToestelId;
                opgehaaldToestel.Naam = toestel.Naam;
                opgehaaldToestel.AankoopDatum = toestel.AankoopDatum;
                opgehaaldToestel.Kuisproduct = toestel.Kuisproduct;
                opgehaaldToestel.Handleiding = toestel.Handleiding;
                opgehaaldToestel.Actief = true;

                //de methode opvragen om de wijzigingen aan de database toepassen
                toestellenservice.ToestelWijzigen(opgehaaldToestel);

                //terugsturen naar de index pagina
                return RedirectToAction("Index");
            }
            //indien er iets misloopt de edit pagina opnieuw weergeven
            catch
            {
                ViewBag.Message = "Fout";
                return View();
            }
        }    

        [HttpGet]
        public ActionResult ToestelDeactiveren(int id)
        {
            try
            {
                //de delete pagina weergeven met het gekozen toestel
                return View(toestellenservice.ToestelWeergeven(id));
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ToestelDeactiveren(Toestel toestel)
        {
            try
            {
                //toestel ophalen uit de database en deactiveren door actief op false te zetten
                toestel = toestellenservice.ToestelWeergeven(toestel.ToestelId);
                toestel.Actief = false;

                //de methode aanspreken om het toestel te wijzigen
                toestellenservice.ToestelWijzigen(toestel);

                //terugsturen naar de index pagina
                return RedirectToAction("Index");
            }
            //indien er iets misloopt de delete pagina weergeven met het toestel
            catch
            {
                ViewBag.Message = "Fout";
                return View(toestellenservice.ToestelWeergeven(toestel.ToestelId));
            }
        }
        public ActionResult AlleProblemenWeergeven(int id)
        {
            try
            {
                //viewmodel aanmaken met alle problemen van een toestel en het toestel zelf
                ToestelProbleemVM toestelProbleemVM = new ToestelProbleemVM()
                {
                    ProblemenPerToestel = toestellenservice.AlleProblemenWeergevenVanToestel(id),
                    Toestel = toestellenservice.ToestelWeergeven(id)
                };

                //de alleProblemenWeergeven pagina weergeven met het viewmodel
                return View(toestelProbleemVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
            
        }

        
        public ActionResult ProbleemWeergeven(int probleemId, int toestelId)
        {
            try
            {
                //viewmodel aanmaken met een probleem en een toestel
                ToestelProbleemVM toestelProbleemVM = new ToestelProbleemVM()
                {
                    probleem = toestellenservice.EenProbleemWeergeven(probleemId),
                    Toestel = toestellenservice.ToestelWeergeven(toestelId)
                };

                //de problemenWeergeven pagina weergeven met het viewmodel
                return View(toestelProbleemVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        
        [HttpGet]
        public ActionResult ProbleemToevoegen(int toestelId)
        {
            try
            {
                //het viewmodel aanmaken met alle toestellen en het gekozen toestel
                ProbleemMetToestellenVM probleemMetToestellenVM = new ProbleemMetToestellenVM()
                {
                    AlleToestellen = toestellenservice.AlleToestellenWeergeven(),
                    Toestel = toestellenservice.ToestelWeergeven(toestelId)
                };

                //probleemToevoegen pagina weergeven met het viewmodel
                return View(probleemMetToestellenVM);
            }
            //indien er iets misloopt de error pagina weergeven
            catch
            {
                return View("Error");
            }
        }

        
        [HttpPost]
        public ActionResult ProbleemToevoegen(ProbleemMetToestellenVM probleemMetToestellenVM)
        {
            try
            {
                //probleem uit viewmodel halen en de waarden aan de proeprties toekennen 
                ToestelProbleem probleem = new ToestelProbleem()
                {
                    ProbleemId = toestellenservice.probleemIdBepalen(probleemMetToestellenVM.Probleem.ProbleemId),
                    Naam = probleemMetToestellenVM.Probleem.Naam,
                    Omschrijving = probleemMetToestellenVM.Probleem.Omschrijving,
                    ToestelId = probleemMetToestellenVM.Probleem.ToestelId
                };

                //de methode aanspreken om het probleem toe te voegen aan de database
                toestellenservice.ProbleemToevoegen(probleem);

                //terugsturen naar de database
                return RedirectToAction("Index");
            }
            //indien er iets misloopt de probleemToevoegen pagina opnieuw weergeven
            catch
            {
                ViewBag.Message = "Fout";
                return View(probleemMetToestellenVM);
            }
        }

        
        [HttpGet]
        public ActionResult ProbleemWijzigen(int probleemId, int toestelId)
        {
            try
            {
                //viewmodel aanmaken met alle toestellen, het probleem en het toestel
                ProbleemMetToestellenVM probleemMetToestellenVM = new ProbleemMetToestellenVM()
                {
                    AlleToestellen = toestellenservice.AlleToestellenWeergeven(),
                    Probleem = toestellenservice.EenProbleemWeergeven(probleemId),
                    Toestel = toestellenservice.ToestelWeergeven(toestelId)
                };
                return View(probleemMetToestellenVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult ProbleemWijzigen(ProbleemMetToestellenVM probleemMetToestellenVM)
        {
            try
            {
                //methode aanroepen om probleem te wijzigen, dat rechtstreeks uit het viewmodel komt
                toestellenservice.ProbleemWijzigen(probleemMetToestellenVM.Probleem);

                //terugsturen naar de details pagina van het probleem
                return RedirectToAction("probleemWeergeven", new { probleemId = probleemMetToestellenVM.Probleem.ProbleemId, toestelId = probleemMetToestellenVM.Probleem.ToestelId });
            }
            //idien er iets misloopt wordt de probleemWijzigen pagina opnieuw weergegeven
            catch
            {
                ViewBag.Message = "Fout";
                return View(probleemMetToestellenVM);
            }            
        }
        public ActionResult DownloadHandleiding(int id)
        {
            //toestel ophalen uit de database a.d.h.v. meegegeven id
            Toestel toestel = toestellenservice.ToestelWeergeven(id);

            //zipfile aanmaken en de gebruiker laten downloaden
            using (ZipFile zip = new ZipFile())
            {
                zip.AddFile(toestel.Handleiding);
                zip.Save(Server.MapPath($"~/myZipFile.zip"));
            }
            Response.ContentType = "application/zip";
            Response.AppendHeader("Content-disposition", $"attachment;filename=\"{toestel.Naam}\"");
            Response.TransmitFile(Server.MapPath("~/myZipFile.zip"));
            Response.End();

            //terugsturen naar de details pagina van de toestellen
            return RedirectToAction("Details");
        }
    }
}
