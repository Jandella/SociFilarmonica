using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Data
{
    public static class ContextHelpers
    {
        /// <summary>
        /// Gestisco una sola anagrafica
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static Task<AnagraficaFilarmonica> GetAnagrafica(this FilarmonicaContext ctx)
        {
            return ctx.Anagrafica.FirstOrDefaultAsync();
        }
    }
}
