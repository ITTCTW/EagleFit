using EL.Models;

namespace DAL.Interfaces
{
    public interface IUserService
    {
        ApplicationUser UserOphalen(int id);
        void UserRoleWijzigen(int id, string role);
        void UserRoleVerwijderen(int id, string role);
    }
}
