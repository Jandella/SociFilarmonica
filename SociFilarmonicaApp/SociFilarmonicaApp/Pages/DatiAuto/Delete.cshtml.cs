using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Models;
using SociFilarmonicaApp.ViewModels;

namespace SociFilarmonicaApp.Pages.DatiAuto
{
    public class DeleteModel : InfoAutoPageModel
    {

        public DeleteModel(FilarmonicaContext context) : base(context)
        {
        }

        

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            InfoAuto = await GetViewModel(id);
            

            if (InfoAuto == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbAuto = await _context.InfoAutomobili.FindAsync(id);

            if (dbAuto != null)
            {
                _context.InfoAutomobili.Remove(dbAuto);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
