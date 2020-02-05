using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Models
{
    public class Socio
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Socio n°")]
        public int NumeroSocio { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100)]
        public string Cognome { get; set; }
        [StringLength(50)]
        public string CodiceFiscale { get; set; }
        [StringLength(50)]
        public string Telefono { get; set; }
        [StringLength(50)]
        public string Telefono2 { get; set; }
        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; }
        [EmailAddress]
        public string Email2 { get; set; }
        /// <summary>
        /// Via + numero civico
        /// </summary>
        [StringLength(100)]
        public string Indirizzo { get; set; }
        [StringLength(50)]
        public string Cap { get; set; }
        [StringLength(100)]
        public string Citta { get; set; }
        [StringLength(50)]
        public string NumeroTesseraAmbima { get; set; }
        [StringLength(100)]
        public string TargaMacchina { get; set; }
        [StringLength(100)]
        public string DescrizioneMacchina { get; set; }

        public int? DatiAutoID { get; set; }
        public int? TipologiaSocioID { get; set; }

        public TipologiaSocio Tipologia { get; set; }
        public InfoAuto DatiAuto { get; set; }

        public ICollection<Quote> RegistroQuote { get; set; }

        public bool PrivacyFirmata { get; set; }
        public bool Annullato { get; set; }
        public DateTime? DataNascita { get; set; }
        [StringLength(100)]
        public string LuogoNascita { get; set; }
    }
}
