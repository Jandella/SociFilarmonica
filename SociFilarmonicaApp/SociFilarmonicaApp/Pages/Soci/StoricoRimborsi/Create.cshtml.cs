using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Data.DbModels;

namespace SociFilarmonicaApp.Pages.StoricoRimborsi
{
    public class CreateModel : PageModel
    {
        private readonly SociFilarmonicaApp.Data.FilarmonicaContext _context;

        public CreateModel(SociFilarmonicaApp.Data.FilarmonicaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RimborsoKm RimborsoKm { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RimborsoKm.Add(RimborsoKm);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
