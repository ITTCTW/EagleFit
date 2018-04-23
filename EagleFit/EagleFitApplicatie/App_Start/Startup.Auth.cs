using DAL.Services;
using EL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using static EagleFitApplicatie.App_Start.IdentityConfig;

namespace EagleFitApplicatie.App_Start
{

    public partial class Startup
	{
        private LedenService ledenService = new LedenService();
        private AbonnementenService abonnementsService = new AbonnementenService();
        private BetalingenService betalingenService = new BetalingenService();
        private PersonenService personenService = new PersonenService();
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            //datum en tijd weergeven van de moment dat de applicatie opstart
            DateTime vandaag = DateTime.Now;
            DateTime vorigeMaand = vandaag.AddMonths(-1);
            //als we de 6de van de maand zijn of indien dit in het weeken ligt we de 7de of 8ste zijn:
            if(vandaag.Day == 7 && vandaag.DayOfWeek == DayOfWeek.Monday || vandaag.Day == 8 && vandaag.DayOfWeek == DayOfWeek.Monday || vandaag.Day == 6)
            {
                //per lid in de database gaan we: 
                foreach (Lid lid in ledenService.AlleLedenWeergeven())
                {
                    lid.Persoon = personenService.PersoonWeergeven(lid.PersoonId);

                    //abonnement van lid opvragen
                    Abonnement abonnement = abonnementsService.AbonnementWeergeven(lid.AbonnementId);

                    //betaling aanmaken
                    Betaling betaling = new Betaling()
                    {
                        Bedrag = abonnement.PrijsPerMaand,
                        Datum = vandaag,
                        Lidnummer = lid.LidNummer,
                        Omschrijving = $"{abonnement.Naam} {vorigeMaand.Month}"                        
                    };
                    betaling.BetalingsId = betalingenService.BetalingsIdBepalen(betaling.BetalingsId);

                    //betaling toevoegen aan database
                    betalingenService.BetalingToevoegen(betaling);

                    //melding van deze te betalen betaling doorsturen naar e-mail

                    IdentityMessage message = new IdentityMessage()
                    {
                        Body = "Er is een nieuwe factuur voor uw abonnement van vorige maand in Eagle Fit",
                        Destination = lid.Persoon.Email,
                        Subject = "Factuur Eagle Fit"
                    };
                    EmailExtensions.Send(message);

                    //te wijzigen abonnement op 0 staat, zo niet gaan we het abonnement wijzigen naar dit id en zetten we het te wijzigen id op 0, staat dit al op 0 gebeurt er niks
                    if (lid.TeWijzigenAbonnementId != 0)
                    {
                        lid.AbonnementId = lid.TeWijzigenAbonnementId;
                        lid.TeWijzigenAbonnementId = 0;
                        ledenService.LidWijzigen(lid);
                    }
                }
            }
        }
    }
}