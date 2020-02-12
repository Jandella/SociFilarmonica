using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Pages.Shared
{
    public static class HtmlHelpersExtensions
    {
        public static IHtmlContent ToSiNo(this IHtmlHelper htmlHelper, bool yesNo)
        {
            var text = yesNo ? "Sì" : "No";
            return new HtmlString(text);
        }
    }
}
