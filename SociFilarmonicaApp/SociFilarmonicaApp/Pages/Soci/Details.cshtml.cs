using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Mappings;
using SociFilarmonicaApp.Models;
using SociFilarmonicaApp.ViewModels;

namespace SociFilarmonicaApp.Pages.Soci
{
    public class DetailsModel : PageModel
    {
        private readonly Data.FilarmonicaContext _context;

        public DetailsModel(Data.FilarmonicaContext context)
        {
            _context = context;
        }

        public SocioVm Socio { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbSocio = await _context.Soci
                .Include(x => x.Tipologia)
                .Include(x => x.DatiAuto)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (dbSocio == null)
                return NotFound();

            Socio = dbSocio.ToSocioVm();
            
            return Page();
        }
    }
}
