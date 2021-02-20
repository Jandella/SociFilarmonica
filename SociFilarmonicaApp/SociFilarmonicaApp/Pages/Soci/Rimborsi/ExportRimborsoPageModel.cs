using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Data.DbModels;
using SociFilarmonicaApp.Models;
using SociFilarmonicaApp.Models.ExcelModels;
using SociFilarmonicaApp.Pages.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Pages.Soci.Rimborsi
{
    public class ExportRimborsoPageModel : PageModelMsgUtente
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
                Distanza = rimborso.DatiDaSerializzare.Distanza,
                DescrizioneItinerario = rimborso.DatiDaSerializzare.DescrizioneItinerario,
                InfoAutoID = rimborso.DatiDaSerializzare.InfoAutoID,
                RimborsoKm = rimborso.DatiDaSerializzare.RimborsoKm,
                TargaMacchina = rimborso.DatiDaSerializzare.TargaMacchina,
                TipoAuto = rimborso.DatiDaSerializzare.TipoAuto,
                ListaProve = rimborso.DatiDaSerializzare.ListaProve,
                Descrizione = rimborso.Descrizione,
                TotaleReale = rimborso.DatiDaSerializzare.TotaleReale,
                TotaleDovuto = rimborso.DatiDaSerializzare.TotaleDovuto,
            };
            //manage null lists
            if (DatiCalcolo.ListaProve == null || !DatiCalcolo.ListaProve.Any())
            {
                DatiCalcolo.ListaProve = new List<DateTime>
                {
                    DateTime.Now.Date
                };
            }
            DatiCalcolo.AltriCostiAltro = new CalcoloRimborsoAltriCostiVM
            {
                Costo = rimborso.DatiDaSerializzare.AltriCostiAltro?.Costo ?? 0,
                NumRicevute = rimborso.DatiDaSerializzare.AltriCostiAltro?.NumRicevute ?? 0,
                Descrizione = rimborso.DatiDaSerializzare.AltriCostiAltro?.Descrizione ?? DatiCalcolo.AltriCostiAltro.Descrizione, //prende il default
            };
            DatiCalcolo.AltriCostiAutostrada = new CalcoloRimborsoAltriCostiVM
            {
                Costo = rimborso.DatiDaSerializzare.AltriCostiAutostrada?.Costo ?? 0,
                NumRicevute = rimborso.DatiDaSerializzare.AltriCostiAutostrada?.NumRicevute ?? 0,
                Descrizione = rimborso.DatiDaSerializzare.AltriCostiAutostrada?.Descrizione ?? DatiCalcolo.AltriCostiAutostrada.Descrizione, //prende il default
            };
            DatiCalcolo.AltriCostiTreno = new CalcoloRimborsoAltriCostiVM
            {
                Costo = rimborso.DatiDaSerializzare.AltriCostiTreno?.Costo ?? 0,
                NumRicevute = rimborso.DatiDaSerializzare.AltriCostiTreno?.NumRicevute ?? 0,
                Descrizione = rimborso.DatiDaSerializzare.AltriCostiTreno?.Descrizione ?? DatiCalcolo.AltriCostiTreno.Descrizione, //prende il default
            };
            DatiCalcolo.AltriCostiVitto = new CalcoloRimborsoAltriCostiVM
            {
                Costo = rimborso.DatiDaSerializzare.AltriCostiVitto?.Costo ?? 0,
                NumRicevute = rimborso.DatiDaSerializzare.AltriCostiVitto?.NumRicevute ?? 0,
                Descrizione = rimborso.DatiDaSerializzare.AltriCostiVitto?.Descrizione ?? DatiCalcolo.AltriCostiTreno.Descrizione, //prende il default
            };
            DatiCalcolo.AltriCostiMezziPubblici = new CalcoloRimborsoAltriCostiVM
            {
                Costo = rimborso.DatiDaSerializzare.AltriCostiMezziPubblici?.Costo ?? 0,
                NumRicevute = rimborso.DatiDaSerializzare.AltriCostiMezziPubblici?.NumRicevute ?? 0,
                Descrizione = rimborso.DatiDaSerializzare.AltriCostiMezziPubblici?.Descrizione ?? DatiCalcolo.AltriCostiTreno.Descrizione, //prende il default
            };
            DatiCalcolo.AltriCostiParcheggi = new CalcoloRimborsoAltriCostiVM
            {
                Costo = rimborso.DatiDaSerializzare.AltriCostiParcheggi?.Costo ?? 0,
                NumRicevute = rimborso.DatiDaSerializzare.AltriCostiParcheggi?.NumRicevute ?? 0,
                Descrizione = rimborso.DatiDaSerializzare.AltriCostiParcheggi?.Descrizione ?? DatiCalcolo.AltriCostiTreno.Descrizione, //prende il default
            };
            DatiCalcolo.AltriCostiHotel = new CalcoloRimborsoAltriCostiVM
            {
                Costo = rimborso.DatiDaSerializzare.AltriCostiHotel?.Costo ?? 0,
                NumRicevute = rimborso.DatiDaSerializzare.AltriCostiHotel?.NumRicevute ?? 0,
                Descrizione = rimborso.DatiDaSerializzare.AltriCostiHotel?.Descrizione ?? DatiCalcolo.AltriCostiTreno.Descrizione, //prende il default
            };
        }

        public void AttachExcelExportAction(IWebHostEnvironment env, AnagraficaFilarmonica ana = null)
        {
            if (HybridSupport.IsElectronActive)
            {
                Electron.IpcMain.RemoveAllListeners("export-excel-calcola");
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
                            IExcelModel m = new RimborsoKmExcel(env, DatiCalcolo) 
                            { 
                                Intestazione1 = ana?.RagioneSociale??"Società",
                                Intestazione2 = ana?.Citta
                            };
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
