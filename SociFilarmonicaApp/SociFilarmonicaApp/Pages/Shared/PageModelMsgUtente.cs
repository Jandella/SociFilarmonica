using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Pages.Shared
{
    public class PageModelMsgUtente : PageModel
    {
        [BindProperty]
        public string MsgSuccess { get; set; }
        [BindProperty]
        public string MsgError { get; set; }

        protected void ClearMessaggioPerUtente()
        {
            MsgSuccess = "";
            MsgError = "";
        }
    }
}
