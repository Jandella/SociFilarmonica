using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Data.DbModels
{
    public class RimborsoKm
    {
        public int ID { get; set; }
        [Required]
        public int SocioID { get; set; }
        public Socio Socio { get; set; }
        [StringLength(100)]
        public string Descrizione { get; set; }
        public DateTime DataCreazione { get; set; } = DateTime.Now;
        public DateTime DataUltimaModifica { get; set; } = DateTime.Now;
        public string DatiRimborsoSerializzati { get; set; }
    }
}
