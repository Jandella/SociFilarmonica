using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Models
{
    public class InfoAutoVm
    {
        public int ID { get; set; }
        [Display(Name = "Tipo di auto")]
        public string TipoAuto { get; set; }
        public string Carburante { get; set; }
        [Display(Name = "Rimborso per Km")]
        public decimal RimborsoKm { get; set; }
        [Display(Name = "Soci assegnati")]
        public int SociConQuestaAuto { get; set; }
    }
}
