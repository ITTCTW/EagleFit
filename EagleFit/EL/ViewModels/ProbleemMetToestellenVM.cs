using EL.Models;
using System.Collections.Generic;

namespace EL.ViewModels
{
    public class ProbleemMetToestellenVM
    {
        public ToestelProbleem Probleem { get; set; }
        public List<Toestel> AlleToestellen { get; set; }
        public Toestel Toestel { get; set; }
    }
}
