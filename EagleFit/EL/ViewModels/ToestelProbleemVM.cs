using EL.Models;
using System.Collections.Generic;

namespace EL.ViewModels
{
    public class ToestelProbleemVM
    {
        public List<ToestelProbleem> ProblemenPerToestel { get; set; }
        public Toestel Toestel { get; set; }
        public ToestelProbleem probleem { get; set; }

    }
}
