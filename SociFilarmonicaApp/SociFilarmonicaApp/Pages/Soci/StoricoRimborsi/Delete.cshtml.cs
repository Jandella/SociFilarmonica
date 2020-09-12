using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Data.DbModels;

namespace SociFilarmonicaApp.Pages.StoricoRimborsi
{
    public class DeleteModel : PageModel
    {
        private readonly SociFilarmonicaApp.Data.FilarmonicaContext _context;

        public DeleteModel(SociFilarmonicaApp.Data.FilarmonicaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RimborsoKm RimborsoKm { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RimborsoKm = await _context.RimborsoKm.FirstOrDefaultAsync(m => m.ID == id);

            if (RimborsoKm == null)
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

            RimborsoKm = await _context.RimborsoKm.FindAsync(id);

            if (RimborsoKm != null)
            {
                _context.RimborsoKm.Remove(RimborsoKm);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
