using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Pages.Soci
{
    public class TipologiePageModel : PageModel
    {
        public SelectList TipologieSL { get; set; }

        public void PopulateTipologieDropDownList(FilarmonicaContext context, object selectedTipologia = null)
        {
            var query = context.TipologiaSoci.OrderBy(x => x.Descrizione);
            TipologieSL = new SelectList(query.AsNoTracking(), "ID", "Descrizione", selectedTipologia);
        }
    }
}
