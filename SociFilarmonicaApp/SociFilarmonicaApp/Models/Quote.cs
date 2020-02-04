using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Models
{
    public class Quote
    {
        public int ID { get; set; }
        public int SocioID { get; set; }
        public Socio Socio { get; set; }
        public decimal QuotaSociale { get; set; }
        public int Anno { get; set; }
    }
}
