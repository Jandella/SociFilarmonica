using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Data.DbModels;
using SociFilarmonicaApp.Pages.Soci.Rimborsi;

namespace SociFilarmonicaApp.Pages.StoricoRimborsi
{
    public class DetailsModel : ExportRimborsoPageModel
    {
        private readonly FilarmonicaContext _context;

        public DetailsModel(FilarmonicaContext context)
        {
            _context = context;
        }

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
    }
}
