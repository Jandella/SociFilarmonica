﻿using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NLog;

namespace SociFilarmonicaApp.Models.ExcelModels
{
    /// <summary>
    /// Modello excel con tutte le posizioni e placeholder
    /// </summary>
    public class RimborsoKmExcel : IExcelModel
    {
        private readonly IWebHostEnvironment env;
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

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
        public string TotaleRealePlaceHolder => "G31";
        public string RicevuteAutostradaPlaceHolder => "C24";
        public string CostoAutostradaPlaceHolder => "G24";
        public string RicevuteTrenoPlaceHolder => "C22";
        public string CostoTrenoPlaceHolder => "G22";
        public string RicevuteAltroPlaceHolder => "C23";
        public string CostoAltroPlaceHolder => "G23";

        private readonly string FileName = "Template_RimborsoSpeseViaggio.xlsx";

        public Task<byte[]> GetBytes()
        {
            return Task.Run(() => 
            {
                var templatePath = Path.Combine(ExcelModelConstant.GetTemplatePath(env), FileName);
                _logger.Debug(templatePath);
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
                _logger.Debug(templatePath);
                using (var p = new ExcelPackage(new FileInfo(templatePath)))
                {
                    ComponiFile(p);
                    p.SaveAs(new FileInfo(path));
                }
            });
        }


        private void ComponiFile(ExcelPackage p)
        {
            try
            {
                var foglioDiCalcolo = p.Workbook.Worksheets.First();
                foglioDiCalcolo.Cells[Intestazione1PlaceHolder].Value = Intestazione1?.ToUpper();
                foglioDiCalcolo.Cells[Intestazione2PlaceHolder].Value = Intestazione2?.ToUpper();

                //anagrafica
                foglioDiCalcolo.Cells[NominativoPlaceHolder].Value = $"{DatiRimborso.Nome} {DatiRimborso.Cognome}";
                foglioDiCalcolo.Cells[DescrizionePlaceHolder].Value = DatiRimborso.Descrizione;
                foglioDiCalcolo.Cells[ItinerarioPlaceHolder].Value = DatiRimborso.DescrizioneItinerario;
                foglioDiCalcolo.Cells[DateViaggiPlaceHolder].Value = string.Join(", ", DatiRimborso.ListaProve.Select(x => x.ToString("dd/MM/yyyy")));

                //dettagli sui km
                foglioDiCalcolo.Cells[KmPercorsiPlaceHolder].Value = DatiRimborso.Distanza;
                foglioDiCalcolo.Cells[CostoKmPlaceHolder].Value = DatiRimborso.RimborsoKm;
                foglioDiCalcolo.Cells[TotaleCostoKmPlaceHolder].Value = DatiRimborso.TotaleReale;

                //anagrafica auto
                foglioDiCalcolo.Cells[ModelloAutoPlaceHolder].Value = DatiRimborso.TipoAuto;
                foglioDiCalcolo.Cells[AlimentazionePlaceHolder].Value = DatiRimborso.Carburante;

                //altri costi 
                foglioDiCalcolo.Cells[CostoTrenoPlaceHolder].Value = DatiRimborso.AltriCostiTreno.Costo;
                foglioDiCalcolo.Cells[RicevuteTrenoPlaceHolder].Value = DatiRimborso.AltriCostiTreno.NumRicevute;
                foglioDiCalcolo.Cells[CostoAutostradaPlaceHolder].Value = DatiRimborso.AltriCostiAutostrada.Costo;
                foglioDiCalcolo.Cells[RicevuteAutostradaPlaceHolder].Value = DatiRimborso.AltriCostiAutostrada.NumRicevute;
                foglioDiCalcolo.Cells[CostoAltroPlaceHolder].Value = DatiRimborso.AltriCostiAltro.Costo;
                foglioDiCalcolo.Cells[RicevuteAltroPlaceHolder].Value = DatiRimborso.AltriCostiAltro.NumRicevute;

                //totale
                foglioDiCalcolo.Cells[TotaleRealePlaceHolder].Value = DatiRimborso.TotaleDovuto;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Dati rimborso: {@datirimborso}, Intestazione1: {i1}, Intestazione2: {i2}", DatiRimborso, Intestazione1, Intestazione2);
                throw;
            }
        }
    }


}
