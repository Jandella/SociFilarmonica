using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.ViewModels;

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
                TipoAuto = socio.DatiAuto.TipoAuto
            };
            return Page();
        }
    }
}