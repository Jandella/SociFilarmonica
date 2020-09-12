using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Data.DbModels;

namespace SociFilarmonicaApp.Pages.TipiSoci
{
    public class DeleteModel : PageModel
    {
        private readonly FilarmonicaContext _context;

        public DeleteModel(FilarmonicaContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TipologiaSocio = await _context.TipologiaSoci.FindAsync(id);

            if (TipologiaSocio != null)
            {
                _context.TipologiaSoci.Remove(TipologiaSocio);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
