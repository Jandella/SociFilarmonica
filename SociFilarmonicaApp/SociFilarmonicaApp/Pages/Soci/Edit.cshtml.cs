using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Mappings;
using SociFilarmonicaApp.DbModels;
using SociFilarmonicaApp.Models;

namespace SociFilarmonicaApp.Pages.Soci
{
    public class EditModel : TipologiePageModel
    {
        private readonly SociFilarmonicaApp.Data.FilarmonicaContext _context;

        public EditModel(SociFilarmonicaApp.Data.FilarmonicaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public SocioVm Socio { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbSocio = await _context.Soci.FirstOrDefaultAsync(m => m.ID == id);

            if (dbSocio == null)
            {
                return NotFound();
            }

            Socio = dbSocio.ToSocioVm();

            PopulateDropDownLists(_context);
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                PopulateDropDownLists(_context, Socio.TipologiaSocioID, Socio.DatiAutoID);
                return Page();
            }

            var socioDaAggiornare = await _context.Soci.FindAsync(id);
            socioDaAggiornare.DataUltimaModifica = DateTime.Now;

            _context.Entry(socioDaAggiornare).CurrentValues.SetValues(Socio);

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        private bool SocioExists(int id)
        {
            return _context.Soci.Any(e => e.ID == id);
        }
    }
}
