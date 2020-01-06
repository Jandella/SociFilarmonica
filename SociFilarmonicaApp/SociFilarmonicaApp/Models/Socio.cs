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
        [StringLength(100)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100)]
        public string Cognome { get; set; }
        [StringLength(50)]
        public string Telefono { get; set; }
        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(500)]
        public string Indirizzo { get; set; }
        public int? DatiAutoID { get; set; }
        public int? TipologiaSocioID { get; set; }

        public TipologiaSocio Tipologia { get; set; }
        public InfoAuto DatiAuto { get; set; }
    }
}
