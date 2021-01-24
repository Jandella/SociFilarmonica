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
                    foreach (var item in query)
                    {
                        ws.Cells[riga, 1].Value = item.Nome;
                        ws.Cells[riga, 2].Value = item.Cognome;
                        ws.Cells[riga, 3].Value = item.Indirizzo;
                        ws.Cells[riga, 4].Value = item.Telefono;
                        ws.Cells[riga, 5].Value = item.Email;
                        ws.Cells[riga, 6].Value = item.Tipologia?.Descrizione;
                        ws.Cells[riga, 7].Value = item.DatiAuto?.TipoAuto;
                        ws.Cells[riga, 8].Value = item.DatiAuto?.Carburante;
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