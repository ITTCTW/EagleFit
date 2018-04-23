using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using EL.Models;
using DAL.Services;
using EL.ViewModels;

namespace EagleFitApplicatie.Controllers
{
    public class LedenController : Controller
    {
        private LedenService ledenService = new LedenService();
        private AbonnementenService abonnementenService = new AbonnementenService();
        private ClubsService clubsService = new ClubsService();
        private AdressenService adressenService = new AdressenService();
        private PersonenService persoonsService = new PersonenService();

        // GET: Leden
        public ActionResult Index()
        {
            try
            {
                //list van alle leden weergeven
                List<Lid> leden = ledenService.AlleLedenWeergeven();

                //voor elk lid in de database de persoon, adres van de persoon en de club in hun propertie steken
                foreach (var lid in leden)
                {
                    lid.Persoon = persoonsService.PersoonWeergeven(lid.PersoonId);
                    lid.Persoon.Adres = adressenService.AdresWeergeven(lid.Persoon.AdresId);
                    lid.Club = clubsService.ClubWeergeven(lid.ClubId);
                }

                //de index pagina weergeven met de lijst van leden
                return View(leden);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // GET: Leden/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                //Het gekozen lid weergeven en de persoon, het adres en de club van het lid eraan toekennen
                Lid lid = ledenService.LidWeergeven(id);
                lid.Persoon = persoonsService.PersoonWeergeven(lid.PersoonId);
                lid.Persoon.Adres = adressenService.AdresWeergeven(lid.Persoon.AdresId);
                lid.Club = clubsService.ClubWeergeven(lid.ClubId);

                //viewmodel aanmaken met lid, abonnement en clubadres
                LidDetailsVM lidDetailsVM = new LidDetailsVM()
                {
                    Lid = lid,
                    Abonnement = abonnementenService.AbonnementWeergeven(lid.AbonnementId),
                    ClubAdres = adressenService.AdresWeergeven(lid.Club.AdresId)
                };

                //details pagina weergeven met de gegevens in het viewmodel
                return View(lidDetailsVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // GET: Leden/Create
        public ActionResult Create(int? id)
        {
            try
            {
                //viewmodel aanmaken met alle gegevens
                LedenAanmakenVM ledenAanmakenVM = new LedenAanmakenVM()
                {
                    Clubs = clubsService.AlleClubsWeergeven(),
                    AbonnementId = Convert.ToInt32(id),
                    Abonnementen = abonnementenService.AlleAbonnementenWeergeven(),
                    MyLid = new Lid() { Persoon = new Persoon() { Adres = new Adres() } }
                };

                //create pagina weergeven met het viewmodel
                return View(ledenAanmakenVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LedenAanmakenVM ledenAanmakenVM)
        {
            try
            {
                //adres ophalen uit viewmodel en de waarden toekennen
                Adres adres = new Adres()
                {
                    AdresId = adressenService.AdresIdBepalen(ledenAanmakenVM.MyLid.Persoon.Adres.AdresId),
                    Straat = ledenAanmakenVM.MyLid.Persoon.Adres.Straat,
                    Huisnummer = ledenAanmakenVM.MyLid.Persoon.Adres.Huisnummer,
                    Postcode = ledenAanmakenVM.MyLid.Persoon.Adres.Postcode,
                    Gemeente = ledenAanmakenVM.MyLid.Persoon.Adres.Gemeente,
                    Actief = true
                };

                //persoon ophalen uit viewmodel en de waarden toekennen
                Persoon persoon = new Persoon()
                {
                    PersoonsId = persoonsService.PersoonIdBepalen(ledenAanmakenVM.MyLid.Persoon.PersoonsId),
                    Voornaam = ledenAanmakenVM.MyLid.Persoon.Voornaam,
                    Familienaam = ledenAanmakenVM.MyLid.Persoon.Familienaam,
                    Email = ledenAanmakenVM.MyLid.Persoon.Email,
                    Geslacht = ledenAanmakenVM.MyLid.Persoon.Geslacht,
                    Adres = adres,
                    AdresId = adres.AdresId,
                    TelefoonNummer = ledenAanmakenVM.MyLid.Persoon.TelefoonNummer
                };
                int abonnementId;

                //indien de user die online is een onthaalmedewerker of technische medewerker is het abonnementid hetgene dat uit het abonnementid van het lid in het viewmodel komt, zoniet dan word het abonnementid in het viewmodel gebruikt
                if (User.IsInRole("Onthaalmedewerker") || User.IsInRole("Technische Medewerker"))
                {
                    abonnementId = ledenAanmakenVM.MyLid.AbonnementId;
                }
                else
                {
                    abonnementId = ledenAanmakenVM.AbonnementId;
                }

                //nieuw lid aanmaken en alle gegevens eraan toekennen
                Lid lid = new Lid();

                lid.LidNummer = ledenService.LidNummerBepalen(ledenAanmakenVM.MyLid.LidNummer);
                lid.AbonnementId = abonnementId;
                lid.ClubId = ledenAanmakenVM.MyLid.ClubId;
                lid.Persoon = persoon;
                lid.PersoonId = persoon.PersoonsId;
                lid.Actief = true;

                //adres.Leden = new List<Lid>();
                //adres.Leden.Add(lid);

                //methode aanspreken om het lid toe te voegen aan de database
                ledenService.LidToevoegen(lid);

                //indien alles gelukt is en de user is een onthaalmedewerker worden we teruggestuurd naar de index methode, indien we een andere role hebben dan zien we de details pagina van het aangemaakte lid
                if (User.IsInRole("Onthaalmedewerker"))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction($"Details/{lid.LidNummer}");
            }
            //indien er iets misloopt wordt de create pagina opnieuw weergegeven
            catch
            {
                ViewBag.Message = "Fout";
                return View(ledenAanmakenVM);
            }
        }

        // GET: Leden/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                //gekozen lid weergeven en persoon en adres eraan toekennen a.d.h.v. id
                Lid lid = ledenService.LidWeergeven(id);
                lid.Persoon = persoonsService.PersoonWeergeven(lid.PersoonId);
                lid.Persoon.Adres = adressenService.AdresWeergeven(lid.Persoon.AdresId);

                //viewmodel aanmaken met lid, abonnement en clubs in
                LedenAanmakenVM ledenAanmakenVM = new LedenAanmakenVM()
                {
                    MyLid = lid,
                    Abonnementen = abonnementenService.AlleAbonnementenWeergeven(),
                    Clubs = clubsService.AlleClubsWeergeven()
                };

                //edit pagina weergeven met viewmodel
                return View(ledenAanmakenVM);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        // POST: Leden/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LedenAanmakenVM ledenAanmakenVM)
        {
            try
            {
                //lidadres en lid op actief zetten
                ledenAanmakenVM.MyLid.Persoon.Adres.Actief = true;
                ledenAanmakenVM.MyLid.Actief = true;

                //methode aanspreken om lid, adres en persoon te wijzigen
                adressenService.AdresWijzigen(ledenAanmakenVM.MyLid.Persoon.Adres);
                persoonsService.PersoonWijzigen(ledenAanmakenVM.MyLid.Persoon);
                ledenService.LidWijzigen(ledenAanmakenVM.MyLid);

                //terugsturen naar de index methode
                return RedirectToAction("Index");
            }
            //indien er iets misloopt wordt de edit pagina opnieuw weergegeven met het viewmodel
            catch
            {
                ViewBag.Message = "Fout";
                return View(ledenAanmakenVM.MyLid);
            }
        }

        [HttpGet]
        public ActionResult LidDeactiveren(int id)
        {
            try
            {
                //lid weergeven en persoon, adres eraan toekennen
                Lid lid = ledenService.LidWeergeven(id);
                lid.Persoon = persoonsService.PersoonWeergeven(lid.PersoonId);
                lid.Persoon.Adres = adressenService.AdresWeergeven(lid.Persoon.AdresId);

                //de delete pagina weergeven met het lid
                return View(lid);
            }
            //indien er iets misloopt wordt de error pagina weergegeven
            catch
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult LidDeactiveren(Lid lid)
        {
            try
            {
                //het gekozen lid weergeven en gegevens toekennen waarvan we actief op false zetten
                lid = ledenService.LidWeergeven(lid.LidNummer);
                lid.Actief = false;
                lid.Persoon = persoonsService.PersoonWeergeven(lid.PersoonId);
                lid.Persoon.Adres = adressenService.AdresWeergeven(lid.Persoon.AdresId);
                lid.Persoon.Adres.Actief = false;

                //adres en lidgegevens wijzigen
                adressenService.AdresWijzigen(lid.Persoon.Adres);
                ledenService.LidWijzigen(lid);

                //terugsturen naar de index methode
                return RedirectToAction("Index");
            }
            //indien er iets misloopt wordt de delete pagina opnieuw weergegeven met het lid
            catch
            {
                ViewBag.Message = "Fout";
                return View(lid);
            }
        }
    }
}
