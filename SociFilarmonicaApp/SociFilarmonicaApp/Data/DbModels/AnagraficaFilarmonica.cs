using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Data.DbModels
{
    public class AnagraficaFilarmonica
    {
        public int Id { get; set; }
        [Display(Name = "Descrizione")]
        public string RagioneSociale { get; set; }
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string Cap { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
    }
}
