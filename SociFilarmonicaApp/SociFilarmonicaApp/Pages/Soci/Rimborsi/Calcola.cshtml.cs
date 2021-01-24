using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ElectronNET.API;
using Microsoft.AspNetCore.Hosting;
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
        protected readonly FilarmonicaContext _context;
        private readonly ILogger<CalcolaModel> _logger;
        private IWebHostEnvironment _env;

        public CalcolaModel(FilarmonicaContext context, IWebHostEnvironment env, ILogger<CalcolaModel> logger)
        {
            _context = context;
            _env = env;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int? idSocio, int? idRimborso)
        {
            _logger.LogInformation("OnGetAsync: idSocio = {0}, idRimborso ={1}", idSocio, idRimborso);
            var ana = await _context.GetAnagrafica();
            AttachExcelExportAction(_env, ana);
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
            var anagrafica = await _context.GetAnagrafica();
            string sede = anagrafica?.Citta;
            if (string.IsNullOrEmpty(sede))
                sede = "sede";

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
                DescrizioneItinerario = $"{socio.Citta} - {sede} e ritorno",
                InfoAutoID = socio.DatiAutoID.Value,
                RimborsoKm = socio.DatiAuto.RimborsoKm,
                TargaMacchina = socio.TargaMacchina,
                TipoAuto = socio.DatiAuto.TipoAuto,
                ListaProve = new List<DateTime> { DateTime.Now.Date }
            };
            DatiCalcolo.Descrizione = DatiCalcolo.GeneraDescrizione();
        }



        public IActionResult OnPostAddDataProva()
        {
            ClearMessaggioPerUtente();
            DatiCalcolo.ListaProve.Add(DateTime.Now);
            return Page();
        }
        public IActionResult OnPostRemoveDataProva(int index)
        {
            ClearMessaggioPerUtente();
            if (index >= 0 && index < DatiCalcolo.ListaProve.Count)
            {
                DatiCalcolo.ListaProve.RemoveAt(index);
            }
            return Page();
        }

        public IActionResult OnPostCalcolatrice()
        {
            ClearMessaggioPerUtente();
            //calcola il totale
            var totale = DatiCalcolo.Calcola();
            DatiCalcolo.TotaleReale = totale;
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            ClearMessaggioPerUtente();
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
            inDb.Descrizione = DatiCalcolo.Descrizione;
            inDb.DatiDaSerializzare = new Data.DbModels.DatiCalcoloDaSerializzare
            {
                AltriCostiAltro = new Data.DbModels.AltriCosti
                {
                    Costo = DatiCalcolo.AltriCostiAltro.Costo,
                    Descrizione = DatiCalcolo.AltriCostiAltro.Descrizione,
                    NumRicevute = DatiCalcolo.AltriCostiAltro.NumRicevute
                },
                AltriCostiAutostrada = new Data.DbModels.AltriCosti
                {
                    Costo = DatiCalcolo.AltriCostiAutostrada.Costo,
                    Descrizione = DatiCalcolo.AltriCostiAutostrada.Descrizione,
                    NumRicevute = DatiCalcolo.AltriCostiAutostrada.NumRicevute
                },
                AltriCostiTreno = new Data.DbModels.AltriCosti
                {
                    Costo = DatiCalcolo.AltriCostiTreno.Costo,
                    Descrizione = DatiCalcolo.AltriCostiTreno.Descrizione,
                    NumRicevute = DatiCalcolo.AltriCostiTreno.NumRicevute
                },
                Carburante = DatiCalcolo.Carburante,
                DescrizioneMacchina = DatiCalcolo.DescrizioneMacchina,
                Distanza = DatiCalcolo.Distanza,
                InfoAutoID = DatiCalcolo.InfoAutoID,
                DescrizioneItinerario = DatiCalcolo.DescrizioneItinerario,
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
            MsgSuccess = "Salvato con successo!";
            return Page();
        }


    }
}