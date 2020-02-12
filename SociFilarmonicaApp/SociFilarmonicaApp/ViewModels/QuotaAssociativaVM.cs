using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.ViewModels
{
    public class QuotaAssociativaVM
    {
        public int ID { get; set; }
        public int SocioID { get; set; }
        [Display(Name = "Quota Sociale")]
        public decimal QuotaSociale { get; set; }
        public int Anno { get; set; }
    }

    public class QuoteVM
    {
        public int SocioID { get; set; }
        [Display(Name = "Nome")]
        public string NomeSocio { get; set; }
        [Display(Name = "Cognome")]
        public string CognomeSocio { get; set; }
        public List<QuotaAssociativaVM> ListaQuote { get; set; }
    }
}
