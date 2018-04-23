using EL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EL.ViewModels
{
    public class ReserveringAanmakenVM
    {
        public Reservering Reservering { get; set; }
        public List<Persoon> AlleMedewerkers { get; set; }
        public List<Zaal> AlleZalen { get; set; }
        public Persoon Persoon { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ReserveringsDatum
        {
            get
            {
                return Reservering.Reserveringsdatum;
            }
        }
    }
}
