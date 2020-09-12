using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Data.DbModels
{
    public class RimborsoKm
    {
        public int ID { get; set; }
        public DateTime DataCreazione { get; set; } = DateTime.Now;
        public DateTime DataUltimaModifica { get; set; } = DateTime.Now;
        public string DatiRimborsoSerializzati { get; set; }
    }
}
