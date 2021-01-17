using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Models.ExcelModels
{
    /// <summary>
    /// Modello excel con tutte le posizioni e placeholder
    /// </summary>
    public class RimborsoKmExcel : IExcelModel
    {
        private IWebHostEnvironment env;
        public RimborsoKmExcel(IWebHostEnvironment env, CalcoloRimborsoVM rimborso)
        {
            DatiRimborso = rimborso ?? throw new ArgumentNullException("rimborso");
            this.env = env;
        }
        public RimborsoKmExcel(IWebHostEnvironment env, string intestazione1, string intestazione2, CalcoloRimborsoVM rimborsoVM) : this(env, rimborsoVM)
        {
            Intestazione1 = intestazione1;
            Intestazione2 = intestazione2;
        }
        public CalcoloRimborsoVM DatiRimborso { get; set; }
        public string Intestazione1 { get; set; }
        public string Intestazione2 { get; set; }
        public string Intestazione1PlaceHolder => "B4";
        public string Intestazione2PlaceHolder => "B5";
        public string NominativoPlaceHolder => "C8";
        public string DescrizionePlaceHolder => "C9";
        public string ItinerarioPlaceHolder => "C10";
        public string DateViaggiPlaceHolder => "C11";
        public string KmPercorsiPlaceHolder => "C15";
        public string CostoKmPlaceHolder => "C16";
        public string TotaleCostoKmPlaceHolder => "G16";
        public string ModelloAutoPlaceHolder => "D18";
        public string AlimentazionePlaceHolder => "D19";

        private readonly string FileName = "Template_RimborsoSpeseViaggio.xlsx";

        public Task<byte[]> GetBytes()
        {
            return Task.Run(() => 
            {
                var templatePath = Path.Combine(ExcelModelConstant.GetTemplatePath(env), FileName);
                using (var p = new ExcelPackage(new FileInfo(templatePath)))
                {
                    ComponiFile(p);
                    return p.GetAsByteArray();
                }
            });
            
        }

        public Task SaveAs(string path)
        {
            return Task.Run(() =>
            {
                var templatePath = Path.Combine(ExcelModelConstant.GetTemplatePath(env), FileName);
                using (var p = new ExcelPackage(new FileInfo(templatePath)))
                {
                    ComponiFile(p);
                    p.SaveAs(new FileInfo(path));
                }
            });
        }


        private void ComponiFile(ExcelPackage p)
        {
            var foglioDiCalcolo = p.Workbook.Worksheets.First();
            foglioDiCalcolo.Cells[Intestazione1].Value = Intestazione1?.ToUpper();
            foglioDiCalcolo.Cells[Intestazione2].Value = Intestazione2?.ToUpper();

            //anagrafica
            foglioDiCalcolo.Cells[NominativoPlaceHolder].Value = $"{DatiRimborso.Nome} {DatiRimborso.Cognome}";
            foglioDiCalcolo.Cells[DescrizionePlaceHolder].Value = DatiRimborso.Descrizione;
            foglioDiCalcolo.Cells[ItinerarioPlaceHolder].Value = DatiRimborso.DescrizioneItinerario;
            foglioDiCalcolo.Cells[DateViaggiPlaceHolder].Value = string.Join(",", DatiRimborso.ListaProve.Select(x => x.ToString("dd/MM/yyyy")));

            //dettagli sui km
            foglioDiCalcolo.Cells[KmPercorsiPlaceHolder].Value = DatiRimborso.Distanza;
            foglioDiCalcolo.Cells[CostoKmPlaceHolder].Value = DatiRimborso.RimborsoKm;
            foglioDiCalcolo.Cells[TotaleCostoKmPlaceHolder].Value = DatiRimborso.TotaleReale;

            //anagrafica auto
            foglioDiCalcolo.Cells[ModelloAutoPlaceHolder].Value = DatiRimborso.TipoAuto;
            foglioDiCalcolo.Cells[AlimentazionePlaceHolder].Value = DatiRimborso.Carburante;

            //altri costi (?)

            //totale
        }
    }


}
