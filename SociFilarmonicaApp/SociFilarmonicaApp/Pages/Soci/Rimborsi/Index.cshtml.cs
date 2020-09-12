using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Data.DbModels;

namespace SociFilarmonicaApp.Pages.StoricoRimborsi
{
    public class IndexModel : PageModel
    {
        private readonly SociFilarmonicaApp.Data.FilarmonicaContext _context;

        public IndexModel(SociFilarmonicaApp.Data.FilarmonicaContext context)
        {
            _context = context;
        }

        public IList<RimborsoKm> RimborsoKm { get;set; }

        public async Task OnGetAsync()
        {
            RimborsoKm = await _context.RimborsoKm.ToListAsync();
        }
    }
}
