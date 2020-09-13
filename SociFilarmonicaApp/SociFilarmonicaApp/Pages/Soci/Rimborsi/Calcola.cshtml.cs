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
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Models;
using SociFilarmonicaApp.Pages;
using SociFilarmonicaApp.Pages.Soci.Rimborsi;

namespace SociFilarmonicaApp
{
    public class CalcolaModel : ExportRimborsoPageModel
    {
        private readonly FilarmonicaContext _context;

        public CalcolaModel(FilarmonicaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? idSocio, int? idRimborso)
        {
            if (idSocio == null && idRimborso == null)
            {
                return NotFound();
            }

            try
            {
                if (idRimborso != null)
                {
                    await GetExisting(idRimborso.Value);
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
        private async Task GetExisting(int idRimborso)
        {
            var rimborso = await _context.RimborsoKm
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