using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Pages.DatiAuto
{
    public class InfoAutoPageModel : PageModel
    {
        protected readonly SociFilarmonicaApp.Data.FilarmonicaContext _context;

        public InfoAutoPageModel(SociFilarmonicaApp.Data.FilarmonicaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InfoAutoVm InfoAuto { get; set; }

        protected async Task<InfoAutoVm> GetViewModel(int? id)
        {
            var dbAuto = await _context.InfoAutomobili.FirstOrDefaultAsync(m => m.ID == id);


            if (dbAuto == null)
            {
                return null;
            }

            var model = new InfoAutoVm
            {
                Carburante = dbAuto.Carburante,
                ID = dbAuto.ID,
                RimborsoKm = dbAuto.RimborsoKm,
                TipoAuto = dbAuto.TipoAuto,
                SociConQuestaAuto = await _context.Soci.Where(x => x.DatiAutoID == id).CountAsync()
            };

            return model;
        }
    }
}
