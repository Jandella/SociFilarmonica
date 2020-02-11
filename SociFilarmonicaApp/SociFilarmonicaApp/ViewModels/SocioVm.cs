using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.ViewModels
{
    public class SocioVm
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        [Display(Name = "Telefono 1")]
        public string Telefono { get; set; }
        [StringLength(50)]
        [Display(Name = "Telefono 2")]
        public string Telefono2 { get; set; }
        [Display(Name = "Indirizzo di posta 1")]
        public string Email { get; set; }
        [Display(Name = "Indirizzo di posta 2")]
        [EmailAddress]
        public string Email2 { get; set; }
        [Display(Name = "Tipologia Socio")]
        public int? TipologiaSocioID { get; set; }
        [Display(Name = "Tipologia Socio")]
        public string TipologiaSocioDesc { get; set; }
        [Display(Name = "Fascia Auto")]
        public int? DatiAutoID { get; set; }
        [Display(Name = "Fascia Auto")]
        public string TipoAutoDesc { get; set; }
        [Required]
        [Display(Name = "N°")]
        public int NumeroSocio { get; set; }
        [Display(Name = "C. F.")]
        [StringLength(50)]
        public string CodiceFiscale { get; set; }
        /// <summary>
        /// Via + numero civico
        /// </summary>
        [StringLength(100)]
        [Display(Name = "In")]
        public string Indirizzo { get; set; }
        [StringLength(50)]
        [Display(Name = "CAP")]
        public string Cap { get; set; }
        [Display(Name = "Residente a")]
        [StringLength(100)]
        public string Citta { get; set; }
        [StringLength(50)]
        [Display(Name = "N° tessera AMBIMA")]
        public string NumeroTesseraAmbima { get; set; }
        [StringLength(100)]
        [Display(Name = "Targa")]
        public string TargaMacchina { get; set; }
        [StringLength(100)]
        [Display(Name = "Modello veicolo")]
        public string DescrizioneMacchina { get; set; }
        [Display(Name = "Consenso Privacy")]
        public bool PrivacyFirmata { get; set; }
        public bool Annullato { get; set; }
        [Display(Name = "Data di nascita")]
        [DataType(DataType.Date)]
        public DateTime? DataNascita { get; set; }
        [Display(Name = "Luogo di nascita")]
        [StringLength(100)]
        public string LuogoNascita { get; set; }
    }
}
