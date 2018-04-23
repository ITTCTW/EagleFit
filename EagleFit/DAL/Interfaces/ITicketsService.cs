using EL.Models;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    interface ITicketsService
    {
        List<Ticket> AlleTicketsWeergeven();
        Ticket TicketWeergeven(int id);
        void TicketToevoegen(Ticket ticket);
        void TicketWijzigen(Ticket ticket);
        void TicketVerwijderen(int id);
        int TicketIdBepalen(int id);
    }
}
