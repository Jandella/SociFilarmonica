using System;
using System.Collections.Generic;
using System.Text;

namespace SociFilarmonicaDesktop.Models
{
    public class TipologiaSocio
    {
        public int ID { get; set; }
        public string Descrizione { get; set; }

        public ICollection<Socio> SociDiCategoria { get; set; }
    }
}
