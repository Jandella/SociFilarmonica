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
        public SelectList AutoSL { get; set; }

        public void PopulateDropDownLists(FilarmonicaContext context, object selectedTipologia = null, object selectedAuto = null)
        {
            var query = context.TipologiaSoci.OrderBy(x => x.Descrizione);
            TipologieSL = new SelectList(query.AsNoTracking(), "ID", "Descrizione", selectedTipologia);

            var auto = context.InfoAutomobili.OrderBy(x => x.TipoAuto)
                .AsNoTracking()
                .Select(x => new KeyValuePair<int, string>(x.ID, $"{x.TipoAuto} - {x.Carburante}"));
            AutoSL = new SelectList(auto, "Key", "Value", selectedAuto);
        }
    }
}
