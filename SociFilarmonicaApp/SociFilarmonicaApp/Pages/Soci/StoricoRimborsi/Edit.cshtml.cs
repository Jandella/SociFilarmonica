using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Data.DbModels;

namespace SociFilarmonicaApp.Pages.StoricoRimborsi
{
    public class EditModel : PageModel
    {
        private readonly SociFilarmonicaApp.Data.FilarmonicaContext _context;

        public EditModel(SociFilarmonicaApp.Data.FilarmonicaContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(RimborsoKm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RimborsoKmExists(RimborsoKm.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RimborsoKmExists(int id)
        {
            return _context.RimborsoKm.Any(e => e.ID == id);
        }
    }
}
