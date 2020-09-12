using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Models;

namespace SociFilarmonicaApp
{
    public class CalcolaModel : PageModel
    {
        private readonly FilarmonicaContext _context;

        public CalcolaModel(FilarmonicaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CalcoloRimborsoVM DatiCalcolo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var socio = await _context.Soci
                .Include(x => x.DatiAuto)
                .FirstOrDefaultAsync(x => x.ID == id);

            if (socio == null || socio.DatiAutoID == null)
            {
                return NotFound();
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
                ListaProve = new List<DateTime> { DateTime.Now },
                AltriCosti = new List<CalcoloRimborsoAltriCostiVM> { new CalcoloRimborsoAltriCostiVM() }
            };
            return Page();
        }

        public IActionResult OnPostAddDataProva()
        {
            DatiCalcolo.ListaProve.Add(DateTime.Now);
            return Page();
        }
        public IActionResult OnPostRemoveDataProva(int index)
        {
            if(index >= 0 && index < DatiCalcolo.ListaProve.Count)
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
        public IActionResult OnPost()
        {
            //calcola il totale
            //todo: salva
            return Page();
        }
        
        public IActionResult OnPostGeneraExcel()
        {
            //TODO: stampa excel
            return Page();
        }
    }
}