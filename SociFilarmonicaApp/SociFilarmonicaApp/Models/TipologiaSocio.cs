using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Models
{
    public class TipologiaSocio
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Descrizione { get; set; }

        public ICollection<Socio> SociDiCategoria { get; set; }
    }
}
