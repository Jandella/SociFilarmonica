using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using SociFilarmonicaApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Pages.Soci.Rimborsi
{
    public class ExportRimborsoPageModel : PageModel
    {
        [BindProperty]
        public CalcoloRimborsoVM DatiCalcolo { get; set; }

        public void ExcelExport()
        {
            if (HybridSupport.IsElectronActive)
            {
                Electron.IpcMain.On("export-excel", async (args) =>
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



                        var path = await Electron.Dialog.ShowSaveDialogAsync(mainWindow, saveOptions);

                        if (!string.IsNullOrEmpty(path))
                        {
                            return;
                        }

                        try
                        {
                            //TODO: crea l'excel
                            using (var p = new ExcelPackage())
                            {

                                var ws = p.Workbook.Worksheets.Add("Rimborso");
                                int riga = 0;
                                //TODO: metti i dati secondo il modello


                                p.SaveAs(new FileInfo(path));
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
