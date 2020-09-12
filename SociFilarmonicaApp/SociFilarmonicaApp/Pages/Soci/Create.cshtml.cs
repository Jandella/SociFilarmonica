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
using SociFilarmonicaApp.Models;

namespace SociFilarmonicaApp.Pages.Soci
{
    public class CreateModel : TipologiePageModel
    {
        private readonly FilarmonicaContext _context;

        public CreateModel(FilarmonicaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateDropDownLists(_context);
            return Page();
        }

        [BindProperty]
        public SocioVm Socio { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.

        //The SetValues method sets the values of this object by reading values from another PropertyValues object. 
        //SetValues uses property name matching. The view model type doesn't need to be related to the model type, 
        //it just needs to have properties that match.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var entry = _context.Add(new Socio());
                
                entry.CurrentValues.SetValues(Socio);

                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                PopulateDropDownLists(_context, Socio.TipologiaSocioID);
                return Page();
            }
        }
    }
}
