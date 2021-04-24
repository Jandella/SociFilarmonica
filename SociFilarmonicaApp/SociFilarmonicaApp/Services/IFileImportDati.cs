using SociFilarmonicaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Services
{
    public interface IFileImportDati<T>
    {
        Task Import(ImportVM datiImport);
    }
}
