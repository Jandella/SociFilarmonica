using System;
using System.Collections.Generic;
using System.Text;

namespace SociFilarmonicaDesktop.Models
{
    public class Socio
    {
        public int ID { get; set; }
       
        public string Nome { get; set; }
        
        public string Cognome { get; set; }
        
        public string Telefono { get; set; }
        
        public string Email { get; set; }
        public int? TipologiaSocioID { get; set; }

        public TipologiaSocio Tipologia { get; set; }
    }
}
