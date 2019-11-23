using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Models;

namespace SociFilarmonicaApp.Pages.Soci
{
    public class DetailsModel : PageModel
    {
        private readonly SociFilarmonicaApp.Data.FilarmonicaContext _context;

        public DetailsModel(SociFilarmonicaApp.Data.FilarmonicaContext context)
        {
            _context = context;
        }

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
    }
}
