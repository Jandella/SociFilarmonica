using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Models;

namespace SociFilarmonicaApp.Pages.Soci
{
    public class DeleteModel : PageModel
    {
        private readonly FilarmonicaContext _context;

        public DeleteModel(FilarmonicaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Socio Socio { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Socio = await _context.Soci.AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);

            if (Socio == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ErrorMessage = "Delete failed. Try again";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Socio = await _context.Soci.FindAsync(id);

            if (Socio == null)
                return NotFound();


            try
            {
                Socio.Annullato = true;
                if(await TryUpdateModelAsync(Socio, "socio", s => s.Annullato))
                {
                    await _context.SaveChangesAsync();
                }
                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return RedirectToAction("./Delete",
                                     new { id, saveChangesError = true });
            }
        }
    }
}
