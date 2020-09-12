using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Data.DbModels;

namespace SociFilarmonicaApp.Pages.TipiSoci
{
    public class IndexModel : PageModel
    {
        private readonly FilarmonicaContext _context;

        public IndexModel(FilarmonicaContext context)
        {
            _context = context;
        }

        public IList<TipologiaSocio> TipologiaSocio { get;set; }

        public async Task OnGetAsync()
        {
            TipologiaSocio = await _context.TipologiaSoci.AsNoTracking().ToListAsync();
        }
    }
}
