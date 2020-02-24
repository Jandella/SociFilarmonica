using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.ViewModels
{
    public class CalcoloRimborsoVM
    {
        public CalcoloRimborsoVM()
        {
            ListaProve = new List<DateTime>();
            AltriCosti = new List<CalcoloRimborsoAltriCostiVM>();
        }
        public int SocioID { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        [Required]
        [Display(Name = "Prove a cui ha partecipato")]
        [DataType(DataType.Date)]
        public List<DateTime> ListaProve { get; set; }
        [Display(Name = "Numero prove")]
        public int NumeroProve => ListaProve?.Count ?? 0;
        [Display(Name = "Distanza casa-sede")]
        public decimal Distanza { get; set; }
        [Display(Name = "Targa")]
        public string TargaMacchina { get; set; }
        [StringLength(100)]
        [Display(Name = "Modello veicolo")]
        public string DescrizioneMacchina { get; set; }
        public int InfoAutoID { get; set; }
        [Display(Name = "Fascia rimborso")]
        public string TipoAuto { get; set; }
        public string Carburante { get; set; }
        [Display(Name = "Rimborso per Km")]
        public decimal RimborsoKm { get; set; }
        public List<CalcoloRimborsoAltriCostiVM> AltriCosti { get; set; }
        [Display(Name = "Totale")]
        public decimal TotaleReale { get; set; }
        [Display(Name = "Totale dovuto")]
        public decimal TotaleDovuto { get; set; }
    }
    public class CalcoloRimborsoAltriCostiVM
    {
        public string Descrizione { get; set; }
        public decimal Costo { get; set; }
    }
}
