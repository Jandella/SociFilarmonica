using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Models;

namespace SociFilarmonicaApp.Pages.TipiSoci
{
    public class EditModel : PageModel
    {
        private readonly SociFilarmonicaApp.Data.FilarmonicaContext _context;

        public EditModel(SociFilarmonicaApp.Data.FilarmonicaContext context)
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var tipologiaDaAggiornare = await _context.TipologiaSoci.FindAsync(id);
            if(await TryUpdateModelAsync(
                tipologiaDaAggiornare, 
                "TipologiaSocio", 
                t => t.Descrizione))
            {
                await _context.SaveChangesAsync();
            }

            _context.Attach(TipologiaSocio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipologiaSocioExists(TipologiaSocio.ID))
                {
                    return NotFound();
                }
            }

            return Page();
        }

        private bool TipologiaSocioExists(int id)
        {
            return _context.TipologiaSoci.Any(e => e.ID == id);
        }
    }
}
