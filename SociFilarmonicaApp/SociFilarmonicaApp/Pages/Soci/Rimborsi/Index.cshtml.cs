using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Data.DbModels;
using SociFilarmonicaApp.Models;

namespace SociFilarmonicaApp.Pages.StoricoRimborsi
{
    public class IndexModel : PageModel
    {
        private readonly SociFilarmonicaApp.Data.FilarmonicaContext _context;

        public IndexModel(SociFilarmonicaApp.Data.FilarmonicaContext context)
        {
            _context = context;
        }

        public string SocioSort { get; set; }
        public string DescrizioneSort { get; set; }
        public string DataCreazioneSort { get; set; }
        public string DataUltimaModificaSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<ElementoRimborsoVm> RimborsoKm { get;set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            DataCreazioneSort = string.IsNullOrEmpty(sortOrder) ? "DataCreazione_desc" : "";
            SocioSort = sortOrder == "Cognome" ? "Cognome_desc" : "Cognome";
            DescrizioneSort = sortOrder == "Descrizione" ? "Descrizione_desc" : "Descrizione";
            DataUltimaModificaSort = sortOrder == "DataUltimaModifica" ? "DataUltimaModifica_desc" : "DataUltimaModifica";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;


            IQueryable<ElementoRimborsoVm> query = _context.RimborsoKm
                .Select(x => new ElementoRimborsoVm
                {
                    Cognome = x.Socio.Cognome,
                    DataCreazione = x.DataCreazione,
                    DataUltimaModifica = x.DataUltimaModifica,
                    Descrizione = x.Descrizione,
                    IdRimborso = x.ID,
                    Nome = x.Socio.Nome,
                    SocioID = x.SocioID,
                    TotaleDovuto = x.TotaleDovuto
                });


            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Nome.ToUpper().Contains(searchString.ToUpper())
                || x.Cognome.ToUpper().Contains(searchString.ToUpper())
                || x.Descrizione.ToUpper().Contains(searchString.ToUpper()));
            }

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "DataCreazione";
            }



            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }

            //dynamic linq: use EF.Property method to specify the name of the property as a string
            if (descending)
            {
                query = query.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                query = query.OrderBy(e => EF.Property<object>(e, sortOrder));
            }
            int pageSize = 10;

            RimborsoKm = await PaginatedList<ElementoRimborsoVm>.CreateAsync(query.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
