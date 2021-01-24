using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public decimal TotaleDovuto { get; set; }
        public string DatiRimborsoSerializzati { get; set; }
        [NotMapped]
        public DatiCalcoloDaSerializzare DatiDaSerializzare { get; set; }

    }
    [NotMapped]
    public class DatiCalcoloDaSerializzare
    {
        public DatiCalcoloDaSerializzare()
        {
            AltriCostiTreno = new AltriCosti("Treno");
            AltriCostiAutostrada = new AltriCosti("Autostrada");
            AltriCostiAltro = new AltriCosti("Altro");
        }
        public List<DateTime> ListaProve { get; set; }
        public decimal Distanza { get; set; }

        public string TargaMacchina { get; set; }
        public string DescrizioneItinerario { get; set; }

        public string DescrizioneMacchina { get; set; }
        public int InfoAutoID { get; set; }

        public string TipoAuto { get; set; }
        public string Carburante { get; set; }

        public decimal RimborsoKm { get; set; }
        public AltriCosti AltriCostiAutostrada { get; set; }
        public AltriCosti AltriCostiTreno { get; set; }
        public AltriCosti AltriCostiAltro { get; set; }
        public decimal TotaleReale { get; set; }
        public decimal TotaleDovuto { get; set; }
    }

    public class AltriCosti
    {
        public AltriCosti()
        {

        }

        public AltriCosti(string desc)
        {
            Descrizione = desc;
        }
        public string Descrizione { get; set; }
        public int NumRicevute { get; set; }
        public decimal Costo { get; set; }
    }
}
