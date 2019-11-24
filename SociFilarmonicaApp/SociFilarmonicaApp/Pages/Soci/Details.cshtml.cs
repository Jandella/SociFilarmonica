using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

            Socio = await _context.Soci.
                Select(x => new SocioVm
                {
                    Cognome = x.Cognome,
                    Email = x.Email,
                    ID = x.ID,
                    Nome = x.Nome,
                    Telefono = x.Telefono,
                    TipologiaSocioID = x.TipologiaSocioID,
                    TipologiaSocioDesc = x.Tipologia.Descrizione
                })
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Socio == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
