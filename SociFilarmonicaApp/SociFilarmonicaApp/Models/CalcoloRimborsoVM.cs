using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Models
{
    public class ElementoRimborsoVm
    {
        public int? IdRimborso { get; set; }
        public int SocioID { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        [Display(Name = "Data creazione")]
        public DateTime DataCreazione { get; set; }
        [Display(Name = "Data ultima modifica")]
        public DateTime DataUltimaModifica { get; set; }
        public string Descrizione { get; set; }
        [Display(Name = "Totale dovuto")]
        public decimal TotaleDovuto { get; set; }
    }
    public class CalcoloRimborsoVM : ElementoRimborsoVm
    {
        public CalcoloRimborsoVM()
        {
            ListaProve = new List<DateTime>();
            AltriCostiAltro = new CalcoloRimborsoAltriCostiVM("Altro");
            AltriCostiAutostrada = new CalcoloRimborsoAltriCostiVM("Autostrada");
            AltriCostiTreno = new CalcoloRimborsoAltriCostiVM("Treno");
            AltriCostiMezziPubblici = new CalcoloRimborsoAltriCostiVM("Mezzi pubblici");
            AltriCostiParcheggi = new CalcoloRimborsoAltriCostiVM("Posteggi");
            AltriCostiVitto = new CalcoloRimborsoAltriCostiVM("Vitto");
            AltriCostiHotel = new CalcoloRimborsoAltriCostiVM("Hotel");
        }
        [Required]
        [Display(Name = "Prove a cui ha partecipato")]
        [DataType(DataType.Date)]
        public List<DateTime> ListaProve { get; set; }
        [Display(Name = "Numero prove")]
        public int NumeroProve => ListaProve?.Count ?? 0;
        [Display(Name = "Descrizione itinerario")]
        public string DescrizioneItinerario { get; set; }
        [Display(Name = "Distanza itinerario (Km)")]
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
        public CalcoloRimborsoAltriCostiVM AltriCostiTreno { get; set; }
        public CalcoloRimborsoAltriCostiVM AltriCostiAutostrada { get; set; }
        public CalcoloRimborsoAltriCostiVM AltriCostiAltro { get; set; }
        public CalcoloRimborsoAltriCostiVM AltriCostiParcheggi { get; set; }
        public CalcoloRimborsoAltriCostiVM AltriCostiVitto { get; set; }
        public CalcoloRimborsoAltriCostiVM AltriCostiMezziPubblici { get; set; }
        public CalcoloRimborsoAltriCostiVM AltriCostiHotel { get; set; }
        [Display(Name = "Totale")]
        public decimal TotaleReale { get; set; }
        

        /// <summary>
        /// Calcola il rimborso come numero di prove per il costo di viaggio andata e ritorno più somma degli altri costi.
        /// <see cref="Distanza"/> * 2 * <see cref="RimborsoKm"/> * <see cref="NumeroProve"/> + altri costi
        /// </summary>
        /// <returns></returns>
        public decimal Calcola()
        {
            var andataRitornoPerUnaProva = Distanza * 2 * RimborsoKm;
            var totaleAndataRitorno = NumeroProve * andataRitornoPerUnaProva;
            var costiAggiuntivi = (AltriCostiAltro?.Costo ?? 0)
                + (AltriCostiAutostrada?.Costo ?? 0)
                + (AltriCostiTreno?.Costo ?? 0)
                + (AltriCostiVitto?.Costo ?? 0)
                + (AltriCostiMezziPubblici?.Costo ?? 0)
                + (AltriCostiParcheggi?.Costo ?? 0)
                + (AltriCostiHotel?.Costo ?? 0);

            return totaleAndataRitorno + costiAggiuntivi;
        }
        /// <summary>
        /// Aiuto per generare la descrizione
        /// </summary>
        /// <returns></returns>
        public string GeneraDescrizione()
        {
            if (!string.IsNullOrEmpty(Descrizione))
                return Descrizione;
            string desc = "Rimborso spesa per socio";
            if (!ListaProve.Any())
            {
                return desc;
            }
            desc = $"Rimborso per prove dal {ListaProve.First():dd/MM/yyyy}";
            if(ListaProve.Count >= 2)
            {
                desc += $" al {ListaProve.Last():dd/MM/yyyy}";
            }
            return desc;
        }

        
    }
    public class CalcoloRimborsoAltriCostiVM
    {
        public CalcoloRimborsoAltriCostiVM()
        {

        }

        public CalcoloRimborsoAltriCostiVM(string desc)
        {
            Descrizione = desc;
        }
        public string Descrizione { get; set; }
        [Display(Name = "Ricevute presentate")]
        public int NumRicevute { get; set; }
        /// <summary>
        /// è il totale
        /// </summary>
        [Display(Name = "Totale spesa")]
        public decimal Costo { get; set; }
        //todo: serve?
        public string Note { get; set; }
    }

    
}
