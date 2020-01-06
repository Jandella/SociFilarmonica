using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.ViewModels
{
    public class SocioVm
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        [Display(Name = "Tipologia Socio")]
        public int? TipologiaSocioID { get; set; }
        [Display(Name = "Tipologia Socio")]
        public string TipologiaSocioDesc { get; set; }
        public int? TipoAutoID { get; set; }
        [Display(Name = "Tipologia Auto")]
        public string TipoAutoDesc { get; set; }
    }
}
