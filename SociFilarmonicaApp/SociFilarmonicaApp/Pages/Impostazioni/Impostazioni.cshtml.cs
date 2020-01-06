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

namespace SociFilarmonicaApp
{
    public class ImpostazioniModel : PageModel
    {
        private readonly Data.FilarmonicaContext _context;
        public ImpostazioniModel(Data.FilarmonicaContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync()
        {
            if (HybridSupport.IsElectronActive)
            {
                //todo: esporta il context nella funzione async
                var query = await _context.Soci
                            .Include(x => x.Tipologia)
                            .Include(x => x.DatiAuto)
                            .AsNoTracking().ToListAsync();

                Electron.IpcMain.On("export-excel", async (args) =>
                {
                    BrowserWindow mainWindow = Electron.WindowManager.BrowserWindows.First();

                    var saveOptions = new SaveDialogOptions
                    {
                        Title = "Save a Excel File",
                        DefaultPath = await Electron.App.GetPathAsync(PathName.documents),
                        Filters = new FileFilter[]
                        {
                        new FileFilter { Name = "xlsx", Extensions = new string[] { "xlsx" } }
                        }
                    };



                    var path = await Electron.Dialog.ShowSaveDialogAsync(mainWindow, saveOptions);

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
                });
            }
        }

        
    }
}