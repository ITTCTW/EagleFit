using DAL.Services;
using EL.Models;
using System.Web.Mvc;

namespace EagleFitApplicatie.Controllers
{
    public class TicketsController : Controller
    {
        private TicketsService ticketsService = new TicketsService(); 
        // GET: Ticket
        public ActionResult Index()
        {
            try
            {
                //als de user een technische medewerker krijgen we de index view te zien met een lijst van alle tickets en als de user een onthaalmedewerker of trainer is krijgt hij de create pagina te zien om een ticket aan te maken aangezien dit het enige is dat deze medewerkers met de tickets moeten doen
                if (User.IsInRole("Technische Medewerker") || User.IsInRole("Admin"))
                {
                    return View(ticketsService.AlleTicketsWeergeven());
                }
                else if (User.IsInRole("Onthaalmedewerker") || User.IsInRole("Trainer"))
                {
                    return RedirectToAction("Create");
                }
                return View("Error");
            }
            //indien er iets misloopt wordt de error pagin
            catch
            {
                return View("Error");
            }
        }

        // GET: Ticket/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                //de details pagina weergeven met het gekozen ticket
                return View(ticketsService.TicketWeergeven(id));
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // GET: Ticket/Create
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

        // POST: Ticket/Create
        [HttpPost]
        public ActionResult Create(Ticket ticket)
        {
            try
            {
                //ticket actief zetten
                ticket.Actief = true;

                //ticket toevoegen aan de database
                ticketsService.TicketToevoegen(ticket);

                //terugsturen naar de index pagina
                return RedirectToAction("Index");
            }
            //indien er iets misloopt wordt de create pagina opnieuw weergegeven
            catch
            {
                ViewBag.Message = "Fout";
                return View();
            }
        }

        // GET: Ticket/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                //de edit pagina weergeven met het gekozen id
                return View(ticketsService.TicketWeergeven(id));
            }
            //indien er iets misloopt de error pagina weergeven
            catch
            {
                return View("Error");
            }
        }

        // POST: Ticket/Edit/5
        [HttpPost]
        public ActionResult Edit(Ticket ticket)
        {
            try
            {
                //ticket op actief zetten
                ticket.Actief = true;

                //ticket wijzigen 
                ticketsService.TicketWijzigen(ticket);

                //terugsturen naar de index pagina
                return RedirectToAction("Index");
            }
            //indien er iets misloopt de edit pagina opnieuw weergeven met het ticket
            catch
            {
                ViewBag.Message = "Fout";
                return View(ticketsService.TicketWeergeven(ticket.TicketId));
            }
        }

        [HttpGet]
        public ActionResult TicketDeactiveren(int id)
        {
            try
            {
                //de delete pagina weergeven met het gekozen ticket
                return View(ticketsService.TicketWeergeven(id));
            }
            //indien er iets misloopt de error pagina weergeven
            catch
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult TicketDeactiveren(Ticket ticket)
        {
            try
            {
                //het ticket ophalen en deactiveren door op false te zetten
                Ticket teWijzigenTicket = ticketsService.TicketWeergeven(ticket.TicketId);
                teWijzigenTicket.Actief = false;

                //de methode oproepen om het ticket te wijzigen
                ticketsService.TicketWijzigen(teWijzigenTicket);

                //terugsturen naar de index pagina
                return RedirectToAction("Index");
            }
            //indien er iets misloopt de view opnieuw weergeven met het ticket
            catch
            {
                ViewBag.Message = "Fout";
                return View(ticketsService.TicketWeergeven(ticket.TicketId));
            }
            
        }
    }
}
