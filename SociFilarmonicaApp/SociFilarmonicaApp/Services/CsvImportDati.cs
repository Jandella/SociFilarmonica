using Microsoft.Extensions.Logging;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Services
{
    public class CsvImportDati<T> : FileImportDatiBase<T>
    {
        public CsvImportDati(FilarmonicaContext ctx, ILogger<CsvImportDati<T>> logger) : base(ctx, logger)
        {

        }
        public override async Task Import(ImportVM datiImport)
        {
            var f = new FileInfo(datiImport.FilePath);
            using (var reader = new StreamReader(f.OpenRead()))
            {
                bool isPrimaRigaIntestazione = datiImport.IsPrimaRigaIntestazione;
                var properties = GetReadWriteProperties();
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();

                    if (isPrimaRigaIntestazione)
                    {
                        isPrimaRigaIntestazione = false;
                    }
                    else
                    {
                        var values = line.Split(datiImport.SeparatoreCsv);
                        T model = (T)Activator.CreateInstance(typeof(T));
                        for (int i = 0; i < values.Length; i++)
                        {
                            var propName = datiImport.Colonne[i];
                            var item = values[i];
                            SetData(model, properties, propName, item);
                        }
                        Aggiungi(model);

                    }
                }
                    await Salva();
            }
        }
    }
}
