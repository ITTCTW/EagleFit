using EL.Models;
using System.Collections.Generic;

namespace EL.ViewModels
{
    public class GroepslessenPerClubVM
    {
        public Club Club { get; set; }
        public List<Groepsles> Groepslessen { get; set; }
    }
}
