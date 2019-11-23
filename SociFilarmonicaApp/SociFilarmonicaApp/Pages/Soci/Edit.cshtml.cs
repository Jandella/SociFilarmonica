using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Models;

namespace SociFilarmonicaApp.Pages.Soci
{
    public class EditModel : PageModel
    {
        private readonly SociFilarmonicaApp.Data.FilarmonicaContext _context;

        public EditModel(SociFilarmonicaApp.Data.FilarmonicaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Socio Socio { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Socio = await _context.Soci.FirstOrDefaultAsync(m => m.ID == id);

            if (Socio == null)
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

            var socioDaAggiornare = await _context.Soci.FindAsync(id);

            if (await TryUpdateModelAsync(
                socioDaAggiornare,
                "socio",
                s => s.Nome, s => s.Cognome, s => s.Telefono))
            {
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            return Page();
        }

        private bool SocioExists(int id)
        {
            return _context.Soci.Any(e => e.ID == id);
        }
    }
}
