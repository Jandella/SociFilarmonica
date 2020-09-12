using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.DbModels;

namespace SociFilarmonicaApp.Pages.TipiSoci
{
    public class DetailsModel : PageModel
    {
        private readonly SociFilarmonicaApp.Data.FilarmonicaContext _context;

        public DetailsModel(SociFilarmonicaApp.Data.FilarmonicaContext context)
        {
            _context = context;
        }

        public TipologiaSocio TipologiaSocio { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TipologiaSocio = await _context.TipologiaSoci.FirstOrDefaultAsync(m => m.ID == id);

            if (TipologiaSocio == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
