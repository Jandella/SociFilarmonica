using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Models;
using SociFilarmonicaApp.Models.ExcelModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Pages.Soci.Rimborsi
{
    public class ExportRimborsoPageModel : PageModel
    {
        

        
        [BindProperty]
        public CalcoloRimborsoVM DatiCalcolo { get; set; }

        protected async Task GetExisting(int idRimborso, FilarmonicaContext context)
        {
            var rimborso = await context.RimborsoKm
                .Include(x => x.Socio)
                .SingleOrDefaultAsync(x => x.ID == idRimborso);
            if (rimborso == null)
            {
                throw new KeyNotFoundException($"Rimborso con chiave {idRimborso} non trovato.");
            }
            rimborso.DatiDaSerializzare = JsonSerializer.Deserialize<Data.DbModels.DatiCalcoloDaSerializzare>(rimborso.DatiRimborsoSerializzati);

            DatiCalcolo = new CalcoloRimborsoVM()
            {
                IdRimborso = rimborso.ID,
                SocioID = rimborso.SocioID,
                Cognome = rimborso.Socio.Cognome,
                Nome = rimborso.Socio.Nome,
                Carburante = rimborso.DatiDaSerializzare.Carburante,
                DescrizioneMacchina = rimborso.DatiDaSerializzare.DescrizioneMacchina,
                Distanza = 0,
                DescrizioneItinerario = rimborso.DatiDaSerializzare.DescrizioneItinerario,
                InfoAutoID = rimborso.DatiDaSerializzare.InfoAutoID,
                RimborsoKm = rimborso.DatiDaSerializzare.RimborsoKm,
                TargaMacchina = rimborso.DatiDaSerializzare.TargaMacchina,
                TipoAuto = rimborso.DatiDaSerializzare.TipoAuto,
                ListaProve = rimborso.DatiDaSerializzare.ListaProve,
                Descrizione = rimborso.Descrizione,
                TotaleReale = rimborso.DatiDaSerializzare.TotaleReale,
                TotaleDovuto = rimborso.DatiDaSerializzare.TotaleDovuto
            };
            //manage null lists
            if (DatiCalcolo.ListaProve == null || !DatiCalcolo.ListaProve.Any())
            {
                DatiCalcolo.ListaProve = new List<DateTime>
                {
                    DateTime.Now.Date
                };
            }
            if (rimborso.DatiDaSerializzare.AltriCosti == null || !rimborso.DatiDaSerializzare.AltriCosti.Any())
            {
                DatiCalcolo.AltriCosti = new List<CalcoloRimborsoAltriCostiVM>() {
                    new CalcoloRimborsoAltriCostiVM()
                };
            }
            else
            {
                DatiCalcolo.AltriCosti = rimborso.DatiDaSerializzare.AltriCosti
                .Select(x => new CalcoloRimborsoAltriCostiVM
                {
                    Costo = x.Costo,
                    Descrizione = x.Descrizione
                }).ToList();
            }
        }

        public void AttachExcelExportAction(IWebHostEnvironment env)
        {
            if (HybridSupport.IsElectronActive)
            {
                Electron.IpcMain.On("export-excel-calcola", async (args) =>
                    {
                        BrowserWindow mainWindow = Electron.WindowManager.BrowserWindows.First();

                        var saveOptions = new SaveDialogOptions
                        {
                            Title = "Save a Excel File",
                            DefaultPath = await Electron.App.GetPathAsync(PathName.Documents),
                            Filters = new FileFilter[]
                            {
                        new FileFilter { Name = "xlsx", Extensions = new string[] { "xlsx" } }
                            }
                        };


                        string path = path = await Electron.Dialog.ShowSaveDialogAsync(mainWindow, saveOptions);
                        

                        if (string.IsNullOrEmpty(path))
                        {
                            return;
                        }

                        try
                        {
                            IExcelModel m = new RimborsoKmExcel(env, DatiCalcolo);
                            await m.SaveAs(path);
                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;
#if DEBUG
                            message = ex.ToString();
#endif
                            Electron.Dialog.ShowErrorBox("Errore", $"Fallita creazione del file. {message}");
                        }
                    });
            }

        }
    }
}
