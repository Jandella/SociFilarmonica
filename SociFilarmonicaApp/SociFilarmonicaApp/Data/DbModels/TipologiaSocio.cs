using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.DbModels
{
    public class TipologiaSocio
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Descrizione { get; set; }
        public DateTime DataCreazione { get; set; } = DateTime.Now;
        public DateTime DataUltimaModifica { get; set; } = DateTime.Now;
        public ICollection<Socio> SociDiCategoria { get; set; }
    }
}
