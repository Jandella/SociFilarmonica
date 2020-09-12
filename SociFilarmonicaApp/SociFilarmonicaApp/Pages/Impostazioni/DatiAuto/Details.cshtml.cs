using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.DbModels;
using SociFilarmonicaApp.Models;

namespace SociFilarmonicaApp.Pages.DatiAuto
{
    public class DetailsModel : InfoAutoPageModel
    {

        public DetailsModel(SociFilarmonicaApp.Data.FilarmonicaContext context) : base(context)
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
    }
}
