using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.DbModels;

namespace SociFilarmonicaApp.Pages.DatiAuto
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
        public InfoAuto InfoAuto { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var entry = _context.Add(new InfoAuto());
            entry.CurrentValues.SetValues(InfoAuto);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
