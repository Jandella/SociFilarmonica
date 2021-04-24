using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.Extensions.Logging;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Data.DbModels;
using SociFilarmonicaApp.Pages.Shared;

namespace SociFilarmonicaApp
{
    public class ImpostazioniModel : PageModelMsgUtente
    {
        private readonly FilarmonicaContext _context;
        private readonly ILogger<ImpostazioniModel> _logger;
        public ImpostazioniModel(FilarmonicaContext context, ILogger<ImpostazioniModel> logger)
        {
            _context = context;
            _logger = logger;
        }
        [BindProperty]
        public AnagraficaFilarmonica Anagrafica { get; set; }
        public async Task OnGetAsync()
        {
            
            Anagrafica = await _context.GetAnagrafica();
            if (Anagrafica == null)
                Anagrafica = new AnagraficaFilarmonica();
            if (HybridSupport.IsElectronActive)
            {
                Electron.IpcMain.RemoveAllListeners("export-excel");
                Electron.IpcMain.On("export-excel", async (args) => await ExportExcelAction(args));
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ClearMessaggioPerUtente();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var model = await _context.GetAnagrafica();
            bool esiste = true;
            if (model == null)
            {
                esiste = false;
                model = new AnagraficaFilarmonica();
            }
            if(await TryUpdateModelAsync<AnagraficaFilarmonica>(
                model, 
                "Anagrafica", 
                a => a.Email, 
                a => a.RagioneSociale,
                a => a.Citta, 
                a => a.Cap,
                a => a.Indirizzo,
                a => a.Tel))
            {
                if(!esiste)
                {
                    _context.Anagrafica.Add(model);
                }
                
                
                await _context.SaveChangesAsync();
            }
            MsgSuccess = "Salvato con successo!";
            return Page();
        }

        private async Task ExportExcelAction(object args)
        {
            var query = await _context.Soci
                        .Include(x => x.Tipologia)
                        .Include(x => x.DatiAuto)
                        .AsNoTracking().ToListAsync();
            _logger.LogInformation("Esporto soci");

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



            var path = await Electron.Dialog.ShowSaveDialogAsync(mainWindow, saveOptions);

            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            try
            {
                using (var p = new ExcelPackage())
                {

                    var ws = p.Workbook.Worksheets.Add("Soci Filarmonica");
                    int riga = 1;
                    //creo intestazione
                    ws.Cells[riga, 1].Value = "Numero socio";
                    ws.Cells[riga, 2].Value = "Tipologia socio";
                    ws.Cells[riga, 3].Value = "Nome";
                    ws.Cells[riga, 4].Value = "Cognome";
                    ws.Cells[riga, 5].Value = "Indirizzo";
                    ws.Cells[riga, 6].Value = "Cap";
                    ws.Cells[riga, 7].Value = "Città";
                    ws.Cells[riga, 8].Value = "Telefono";
                    ws.Cells[riga, 9].Value = "Telefono 2";
                    ws.Cells[riga, 10].Value = "Email";
                    ws.Cells[riga, 11].Value = "Email 2";
                    ws.Cells[riga, 12].Value = "Codice fiscale";
                    ws.Cells[riga, 13].Value = "Numero tessera ambima";
                    ws.Cells[riga, 14].Value = "Data di nascita";
                    ws.Cells[riga, 15].Value = "Luogo di nascita";
                    ws.Cells[riga, 16].Value = "Tipo auto";
                    ws.Cells[riga, 17].Value = "Carburante";
                    ws.Cells[riga, 18].Value = "Descrizione macchina";
                    ws.Cells[riga, 19].Value = "Targa macchina";
                    //metto i dati
                    riga++;
                    foreach (var item in query)
                    {
                        ws.Cells[riga, 1].Value = item.NumeroSocio;
                        ws.Cells[riga, 2].Value = item.Tipologia?.Descrizione;
                        ws.Cells[riga, 3].Value = item.Nome;
                        ws.Cells[riga, 4].Value = item.Cognome;
                        ws.Cells[riga, 5].Value = item.Indirizzo;
                        ws.Cells[riga, 6].Value = item.Cap;
                        ws.Cells[riga, 7].Value = item.Citta;
                        ws.Cells[riga, 8].Value = item.Telefono;
                        ws.Cells[riga, 9].Value = item.Telefono2;
                        ws.Cells[riga, 10].Value = item.Email;
                        ws.Cells[riga, 11].Value = item.Email2;
                        ws.Cells[riga, 12].Value = item.CodiceFiscale;
                        ws.Cells[riga, 13].Value = item.NumeroTesseraAmbima;
                        ws.Cells[riga, 14].Value = item.DataNascita;
                        ws.Cells[riga, 15].Value = item.LuogoNascita;
                        ws.Cells[riga, 16].Value = item.DatiAuto?.TipoAuto;
                        ws.Cells[riga, 17].Value = item.DatiAuto?.Carburante;
                        ws.Cells[riga, 18].Value = item.DescrizioneMacchina;
                        ws.Cells[riga, 19].Value = item.TargaMacchina;
                        riga++;
                    }


                    p.SaveAs(new FileInfo(path));

                    await Electron.Dialog.ShowMessageBoxAsync(mainWindow, "Salvato!");
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
#if DEBUG
                message = ex.ToString();
#endif
                Electron.Dialog.ShowErrorBox("Errore", $"Fallita creazione del file. {message}");
            }
        }
        
    }
}