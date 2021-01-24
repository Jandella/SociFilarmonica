using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Data.DbModels;

namespace SociFilarmonicaApp.Pages.Soci
{
    public class IndexModel : PageModel
    {
        private readonly FilarmonicaContext _context;

        public IndexModel(FilarmonicaContext context)
        {
            _context = context;
        }

        public string NomeSort { get; set; }
        public string CognomeSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Models.SocioVm> Soci { get;set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NomeSort = string.IsNullOrEmpty(sortOrder) ? "Nome_desc" : "";
            CognomeSort = sortOrder == "Cognome" ? "Cognome_desc" : "Cognome";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Socio> sociIQ = _context.Soci
                .Where(s => !s.Annullato);

            if (!string.IsNullOrEmpty(searchString))
            {
                sociIQ = sociIQ.Where(x => x.Nome.ToUpper().Contains(searchString.ToUpper()) 
                || x.Cognome.ToUpper().Contains(searchString.ToUpper()));
            }

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "Nome";
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
                sociIQ = sociIQ.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                sociIQ = sociIQ.OrderBy(e => EF.Property<object>(e, sortOrder));
            }

            int pageSize = 10;
            var sociVmIQ = sociIQ.Select(x => new Models.SocioVm
            {
                NumeroSocio = x.NumeroSocio,
                Cognome = x.Cognome,
                ID = x.ID,
                Nome = x.Nome,
                TipologiaSocioID = x.TipologiaSocioID,
                TipologiaSocioDesc = x.Tipologia.Descrizione,
                PrivacyFirmata = x.PrivacyFirmata,
                DatiAutoID = x.DatiAutoID
            });

            Soci = await PaginatedList<Models.SocioVm>.CreateAsync(sociVmIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
