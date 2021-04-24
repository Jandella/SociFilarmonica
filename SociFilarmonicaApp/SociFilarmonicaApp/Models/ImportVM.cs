using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Models
{
    
    public class ImportVM
    {
        public string FilePath { get; set; }
        /// <summary>
        /// Ordine delle colonne nel file. Colonne[0] -> tipo di dato nella colonna 0
        /// </summary>
        public string[] Colonne { get; set; }
        public bool IsPrimaRigaIntestazione { get; set; }
        public char SeparatoreCsv { get; set; }
    }
}
