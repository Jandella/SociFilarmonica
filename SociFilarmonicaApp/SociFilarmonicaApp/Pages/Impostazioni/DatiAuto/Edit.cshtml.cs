using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Data.DbModels;

namespace SociFilarmonicaApp.Pages.DatiAuto
{
    public class EditModel : PageModel
    {
        private readonly FilarmonicaContext _context;

        public EditModel(FilarmonicaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InfoAuto InfoAuto { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InfoAuto = await _context.InfoAutomobili.FirstOrDefaultAsync(m => m.ID == id);

            if (InfoAuto == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            InfoAuto.DataUltimaModifica = DateTime.Now;

            var infoDaAggiornare = await _context.InfoAutomobili.FindAsync(id);
            infoDaAggiornare.DataUltimaModifica = DateTime.Now;

            _context.Entry(infoDaAggiornare).CurrentValues.SetValues(InfoAuto);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfoAutoExists(InfoAuto.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool InfoAutoExists(int id)
        {
            return _context.InfoAutomobili.Any(e => e.ID == id);
        }
    }
}
