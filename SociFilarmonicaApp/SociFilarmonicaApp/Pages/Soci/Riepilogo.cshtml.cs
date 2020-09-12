using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Models;

namespace SociFilarmonicaApp.Pages
{
    public class RiepilogoModel : PageModel
    {
        private readonly FilarmonicaContext _context;

        public RiepilogoModel(FilarmonicaContext context)
        {
            _context = context;
        }

        public IList<RiepilogoSociVM> SociPerTipologia { get; set; }
        public async Task OnGetAsync()
        {
            var query = _context.TipologiaSoci
                .Select(x => new RiepilogoSociVM 
                {
                    TipoSocio = x.Descrizione,
                    NumSoci = x.SociDiCategoria.Count()
                });

            SociPerTipologia = await query.AsNoTracking().ToListAsync();
        }
    }
}