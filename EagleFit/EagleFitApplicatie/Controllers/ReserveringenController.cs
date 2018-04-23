using DAL.Services;
using EL.Models;
using EL.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EagleFitApplicatie.Controllers
{
    public class ReserveringenController : Controller
    {
        private ReserveringenService reserveringenService = new ReserveringenService();
        private MedewerkersService medewerkersService = new MedewerkersService();
        private ZalenService zalenService = new ZalenService();
        private PersonenService personenService = new PersonenService();
        private ClubsService clubsService = new ClubsService();
        // GET: Reserveringen
        public ActionResult Index()
        {
            try
            {
                //lijst met alle reserveringen weergeven die vandaag of in de toekomst liggen
                List<Reservering> reserveringen = reserveringenService.AlleActieveReserveringenWeergeven();

                //voor elke reservering de medewerker, persoon en zaal aan de propertie toekennen
                foreach (var item in reserveringen)
                {
                    item.Medewerker = medewerkersService.MedewerkerWeergeven(item.MedewerkersId);
                    item.Medewerker.Persoon = personenService.PersoonWeergeven((item.Medewerker.PersoonsId));
                    item.Zaal = zalenService.ZaalWeergeven(item.ZaalId);
                }

                //de index pagina weergeven met de lijst met reserveringen
                return View(reserveringen);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }                                  
        }

        // GET: Reserveringen/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                //reservering weergeven en medewerker, persoon en zaal eraan toekennen
                Reservering reservering = reserveringenService.ReserveringWeergeven(id);
                reservering.Medewerker = medewerkersService.MedewerkerWeergeven(reservering.MedewerkersId);
                reservering.Medewerker.Persoon = personenService.PersoonWeergeven(reservering.Medewerker.PersoonsId);
                reservering.Zaal = zalenService.ZaalWeergeven(reservering.ZaalId);

                //de details pagina weergeven met de reservering
                return View(reservering);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // GET: Reserveringen/Create
        public ActionResult Create()
        {
            try
            {
                //viewmodel aanmaken met alle medewerkers en alle zalen
                ReserveringAanmakenVM reserveringAanmakenVM = new ReserveringAanmakenVM()
                {
                    AlleMedewerkers = medewerkersService.AllePersonenMedewerkersWeergeven(),
                    AlleZalen = zalenService.AlleZalenWeergeven()
                };

                //de create pagina weergeven met het viewmodel
                return View(reserveringAanmakenVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // POST: Reserveringen/Create
        [HttpPost]
        public ActionResult Create(ReserveringAanmakenVM reserveringAanmakenVM)
        {
            try
            {
                //reservering ophalen uit het viewmodel
                Reservering reservering = reserveringAanmakenVM.Reservering;

                //lijst aanmaken met alle reserveringen die bij een zaal horen
                List<Reservering> alleReserveringenPerZaal = reserveringenService.AlleReserveringenPerZaalWeergeven(reservering.ZaalId);

                //hier maken we een datum die de datum is die ingegeven werd door de gebruiker
                DateTime aanTeMakenDatum = new DateTime(reservering.Reserveringsdatum.Year, reservering.Reserveringsdatum.Month, reservering.Reserveringsdatum.Day, reservering.Reserveringsuur.Hours, reservering.Reserveringsuur.Minutes, reservering.Reserveringsuur.Seconds);

                //voor elke reservering in de lijst van reserveringen als de nieuwe reservering minder is dan een uur voor of na het uur van de reservering in de lijst die hij aan het controleren is gaat de viewbag "reservatieTijdBezet" op "Bezet" gezet worden en gaan we een exception gooien zodat we naar de "catch" gaan en dan wordt de pagina opnieuw getoond met een boodschap dat het niet lukt
                foreach (var item in alleReserveringenPerZaal)
                {
                    if (aanTeMakenDatum < item.Reserveringsdatum.AddHours(1) && aanTeMakenDatum > item.Reserveringsdatum.AddHours(-1))
                    {
                        ViewBag.reservatieTijdBezet = "Bezet";
                        throw new Exception();
                    }
                }
                //is de tijd wel goed gaan we een reservatie aanmaken
                reservering.Actief = true;
                reservering.Reserveringsdatum = aanTeMakenDatum;
                reservering.MedewerkersId = medewerkersService.MedewerkersIdAdhvPersoonsIdWeergeven(reserveringAanmakenVM.Persoon.PersoonsId);

                //de reservering toevoegen aan de database
                reserveringenService.ReserveringToevoegen(reservering);

                //terugsturen naar de index pagina
                return RedirectToAction("Index");
            }
            //indien er iets misloopt gaan we opnieuw de viewmodel met gegevens meegeven en de create pagina opnieuw weergeven
            catch
            {
                if(ViewBag.reservatieTijdBezet == null)
                {
                    ViewBag.Message = "Fout";
                }                
                reserveringAanmakenVM.AlleZalen = zalenService.AlleZalenWeergeven();
                reserveringAanmakenVM.AlleMedewerkers = medewerkersService.AllePersonenMedewerkersWeergeven();
                return View(reserveringAanmakenVM);
            }
        }

        // GET: Reserveringen/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                //de gekozen reservering en de medewerker die erbij hoort weergeven
                Reservering reservering = reserveringenService.ReserveringWeergeven(id);
                Medewerker medewerker = medewerkersService.MedewerkerWeergeven(reservering.MedewerkersId);

                //viewmodel aanmaken met reservering, alle medewerkers en alle zalen voor in dropdownlijsten
                ReserveringAanmakenVM reserveringAanmakenVM = new ReserveringAanmakenVM()
                {
                    Reservering = reservering,
                    AlleMedewerkers = medewerkersService.AllePersonenMedewerkersWeergeven(),
                    AlleZalen = zalenService.AlleZalenWeergeven(),
                    Persoon = personenService.PersoonWeergeven(medewerker.PersoonsId)
                };

                //de edit pagina weergeven met het viewmodel
                return View(reserveringAanmakenVM);
            }
            //indien er iets misloopt de error pagina weergeven
            catch
            {
                return View("Error");
            }
        }

        // POST: Reserveringen/Edit/5
        [HttpPost]
        public ActionResult Edit(ReserveringAanmakenVM reserveringAanmakenVM)
        {
            try
            {
                //reservering ophalen uit het viewmodel
                Reservering reservering = reserveringAanmakenVM.Reservering;

                //lijst aanmaken met alle reserveringen per zaal a.d.h.v. het zaalid
                List<Reservering> alleReserveringenPerZaal = reserveringenService.AlleReserveringenPerZaalWeergeven(reservering.ZaalId);

                //de datum uit het viewmodel halen indien deze gewijzigd is
                DateTime aanTeMakenDatum = new DateTime(reservering.Reserveringsdatum.Year, reservering.Reserveringsdatum.Month, reservering.Reserveringsdatum.Day, reservering.Reserveringsuur.Hours, reservering.Reserveringsuur.Minutes, reservering.Reserveringsuur.Seconds);

                //voor elke reservering in de lijst van reserveringen als de nieuwe reservering minder is dan een uur voor of na het uur van de reservering in de lijst die hij aan het controleren is gaat de viewbag "reservatieTijdBezet" op "Bezet" gezet worden en gaan we een exception gooien zodat we naar de "catch" gaan en dan wordt de pagina opnieuw getoond met een boodschap dat het niet lukt
                foreach (var item in alleReserveringenPerZaal)
                {
                    if (aanTeMakenDatum < item.Reserveringsdatum.AddHours(1) && aanTeMakenDatum > item.Reserveringsdatum.AddHours(-1) && item.ReserveringsId != reservering.ReserveringsId)
                    {
                        ViewBag.reservatieTijdBezet = "Bezet";
                        throw new Exception();
                    }
                }

                //de oude reservering ophalen uit de database
                Reservering oudeReservering = reserveringenService.ReserveringWeergeven(reserveringAanmakenVM.Reservering.ReserveringsId);

                //de nieuwe reservering ophalen uit het viewmodel en de propperties eraan toekennen
                Reservering teWijzigenReservering = reserveringAanmakenVM.Reservering;
                teWijzigenReservering.Actief = true;
                teWijzigenReservering.Reserveringsdatum = aanTeMakenDatum;
                teWijzigenReservering.MedewerkersId = medewerkersService.MedewerkersIdAdhvPersoonsIdWeergeven(reserveringAanmakenVM.Persoon.PersoonsId);

                //de methode aanspreken om de reservering te wijzigen
                reserveringenService.ReserveringWijzigen(teWijzigenReservering);

                //terugsturen naar de index pagina
                return RedirectToAction("Index");
            }
            //indien er iets misloopt gaan we opnieuw de viewmodel met gegevens meegeven en de create pagina opnieuw weergeven 
            catch
            {
                if (ViewBag.reservatieTijdBezet == null)
                {
                    ViewBag.Message = "Fout";
                }
                reserveringAanmakenVM.AlleZalen = zalenService.AlleZalenWeergeven();
                reserveringAanmakenVM.AlleMedewerkers = medewerkersService.AllePersonenMedewerkersWeergeven();
                return View(reserveringAanmakenVM);
            }
        }

        [HttpGet]
        public ActionResult ReserveringDeactiveren(int id)
        {
            try
            {
                //de gekozen reservering weergeven a.d.h.v. het id en de bijhorende medewerker en zaal eraan toekennen
                Reservering reservering = reserveringenService.ReserveringWeergeven(id);
                reservering.Medewerker = medewerkersService.MedewerkerWeergeven(reservering.MedewerkersId);
                reservering.Zaal = zalenService.ZaalWeergeven(reservering.ZaalId);

                //de delete pagina weergeven met de reservering
                return View(reservering);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult ReserveringDeactiveren(Reservering reservering)
        {
            try
            {
                //de reservering ophalen uit de database en deze deactiveren door actief op false te zetten
                Reservering oudeReservering = reserveringenService.ReserveringWeergeven(reservering.ReserveringsId);
                oudeReservering.Actief = false;

                //de methode aanspreken om de reservering te wijzigen
                reserveringenService.ReserveringWijzigen(oudeReservering);

                //terugsturen naar de inded pagina
                return RedirectToAction("Index");
            }
            //indien er iets misloopt de delete pagina opnieuw weergeven met de reservering uit de database
            catch
            {
                ViewBag.Message = "Fout";
                return View(reserveringenService.ReserveringWeergeven(reservering.ReserveringsId));
            }
        }
    }
}
