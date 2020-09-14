using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ElectronNET.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Models;
using SociFilarmonicaApp.Pages;
using SociFilarmonicaApp.Pages.Soci.Rimborsi;

namespace SociFilarmonicaApp
{
    public class CalcolaModel : ExportRimborsoPageModel
    {
        private readonly FilarmonicaContext _context;
        private readonly ILogger<CalcolaModel> _logger;

        public CalcolaModel(FilarmonicaContext context, ILogger<CalcolaModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int? idSocio, int? idRimborso)
        {
            _logger.LogInformation("OnGetAsync: idSocio = {0}, idRimborso ={1}", idSocio, idRimborso);
            ExcelExport();
            if (idSocio == null && idRimborso == null)
            {
                _logger.LogInformation("params null, return NOT FOUND");
                return NotFound();
            }

            try
            {
                if (idRimborso != null)
                {
                    await GetExisting(idRimborso.Value, _context);
                }
                else if (idSocio != null)
                {
                    await GetNew(idSocio.Value);
                }

            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }


            return Page();
        }
        private async Task GetNew(int idSocio)
        {
            var socio = await _context.Soci
                .Include(x => x.DatiAuto)
                .FirstOrDefaultAsync(x => x.ID == idSocio);

            if (socio == null || socio.DatiAutoID == null)
            {
                throw new KeyNotFoundException($"Rimborso con chiave {idSocio} non trovato.");
            }

            DatiCalcolo = new CalcoloRimborsoVM()
            {
                SocioID = socio.ID,
                Cognome = socio.Cognome,
                Nome = socio.Nome,
                Carburante = socio.DatiAuto.Carburante,
                DescrizioneMacchina = socio.DescrizioneMacchina,
                Distanza = 0,
                InfoAutoID = socio.DatiAutoID.Value,
                RimborsoKm = socio.DatiAuto.RimborsoKm,
                TargaMacchina = socio.TargaMacchina,
                TipoAuto = socio.DatiAuto.TipoAuto,
                ListaProve = new List<DateTime> { DateTime.Now.Date },
                AltriCosti = new List<CalcoloRimborsoAltriCostiVM> { new CalcoloRimborsoAltriCostiVM() }
            };
        }
        


        public IActionResult OnPostAddDataProva()
        {
            DatiCalcolo.ListaProve.Add(DateTime.Now);
            return Page();
        }
        public IActionResult OnPostRemoveDataProva(int index)
        {
            if (index >= 0 && index < DatiCalcolo.ListaProve.Count)
            {
                DatiCalcolo.ListaProve.RemoveAt(index);
            }
            return Page();
        }

        public IActionResult OnPostAddAltroCosto()
        {
            DatiCalcolo.AltriCosti.Add(new CalcoloRimborsoAltriCostiVM());
            return Page();
        }
        public IActionResult OnPostRemoveAltroCosto(int index)
        {
            if (index >= 0 && index < DatiCalcolo.AltriCosti.Count)
            {
                DatiCalcolo.AltriCosti.RemoveAt(index);
            }
            return Page();
        }

        public IActionResult OnPostCalcolatrice()
        {
            //calcola il totale
            var totale = DatiCalcolo.Calcola();
            DatiCalcolo.TotaleReale = totale;
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            //calcola il totale
            var totale = DatiCalcolo.Calcola();
            DatiCalcolo.TotaleReale = totale;
            //check if esiste 1
            var inDb = await _context.RimborsoKm.SingleOrDefaultAsync(x => x.ID == DatiCalcolo.IdRimborso);
            if (inDb == null)
            {
                inDb = new Data.DbModels.RimborsoKm()
                {
                    DataCreazione = DateTime.Now,
                    SocioID = DatiCalcolo.SocioID
                };
                _context.RimborsoKm.Add(inDb);
            }
            inDb.DataUltimaModifica = DateTime.Now;
            inDb.Descrizione = DatiCalcolo.GeneraDescrizione();
            inDb.DatiDaSerializzare = new Data.DbModels.DatiCalcoloDaSerializzare
            {
                AltriCosti = DatiCalcolo.AltriCosti.Select(x => new Data.DbModels.AltriCosti
                {
                    Costo = x.Costo,
                    Descrizione = x.Descrizione
                }).ToList(),
                Carburante = DatiCalcolo.Carburante,
                DescrizioneMacchina = DatiCalcolo.DescrizioneMacchina,
                Distanza = DatiCalcolo.Distanza,
                InfoAutoID = DatiCalcolo.InfoAutoID,
                ListaProve = DatiCalcolo.ListaProve,
                RimborsoKm = DatiCalcolo.RimborsoKm,
                TargaMacchina = DatiCalcolo.TargaMacchina,
                TipoAuto = DatiCalcolo.TipoAuto,
                TotaleDovuto = DatiCalcolo.TotaleDovuto,
                TotaleReale = DatiCalcolo.TotaleReale
            };
            inDb.TotaleDovuto = DatiCalcolo.TotaleDovuto;
            inDb.DatiRimborsoSerializzati = JsonSerializer.Serialize(inDb.DatiDaSerializzare);
            await _context.SaveChangesAsync();
            return Page();
        }

        public IActionResult OnPostGeneraExcel()
        {
            ExcelExport();
            return Page();
        }
    }
}