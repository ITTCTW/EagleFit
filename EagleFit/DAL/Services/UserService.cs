using DAL.Interfaces;
using EL.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;

namespace DAL.Services
{
    public class UserService : IUserService
    {
        public ApplicationUser UserOphalen(int persoonsId)
        {
            //database connectie openen die automatisch gaat sluiten
            using (ApplicationDbContext DBContext = new ApplicationDbContext())
            {
                //een user teruggeven a.d.h.v. het persoonsid
                return DBContext.Users.Where(user => user.PersoonsId == persoonsId).FirstOrDefault();
            }
        }

        public void UserRoleVerwijderen(int id, string role)
        {
            //database connectie openen die automatisch gaat sluiten
            using (ApplicationDbContext DBContext = new ApplicationDbContext())
            {
                //usermanager aanmaken
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(DBContext));
                try
                {
                    //user weergeven a.d.h.v. het persoonsid
                    ApplicationUser teWijzigenUser = DBContext.Users.Where(user => user.PersoonsId == id).FirstOrDefault();

                    //user van role/status verwijderen
                    userManager.RemoveFromRole(teWijzigenUser.Id, role);

                    //de aanpassingen opslaan
                    DBContext.SaveChanges();
                }

                //exception opvangen indien er iets misgaat
                catch
                {
                    throw;
                }
            }
        }

        public void UserRoleWijzigen(int id, string role)
        {
            //database connectie openen die automatisch gaat sluiten
            using (ApplicationDbContext DBContext = new ApplicationDbContext())
            {
                //usermanager aanmaken
                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(DBContext));
                try
                {
                    //user weergeven a.d.h.v. het persoonsid
                    ApplicationUser user = DBContext.Users.Where(u => u.PersoonsId == id).FirstOrDefault();

                    //user aan role/status toevoegen
                    userManager.AddToRole(user.Id, role);

                    //de aanpassingen opslaan
                    DBContext.SaveChanges();
                }

                //exception opvangen indien er iets misgaat
                catch
                {
                    throw;
                }
            }
        }
    }
}
