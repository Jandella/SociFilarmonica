using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Models.ExcelModels
{
    public static class ExcelModelConstant
    {
        public const string Folder = "ExcelTemplates";
        /// <summary>
        /// Restituisce il full path della cartella dei template
        /// </summary>
        /// <param name="env"></param>
        /// <returns></returns>
        public static string GetTemplatePath(IWebHostEnvironment env)
        {
            return System.IO.Path.Combine(env.ContentRootPath, Folder);
        }
    }
    public interface IExcelModel
    {
        Task SaveAs(string path);
        Task<byte[]> GetBytes();
    }
}
