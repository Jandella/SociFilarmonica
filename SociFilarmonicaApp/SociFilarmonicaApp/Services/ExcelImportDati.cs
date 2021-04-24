using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Services
{
    public class ExcelImportDati<T> : FileImportDatiBase<T>
    {
        public ExcelImportDati(FilarmonicaContext ctx, ILogger<ExcelImportDati<T>> logger) : base(ctx, logger)
        {

        }
        public override async Task Import(ImportVM datiImport)
        {
            using (var p = new ExcelPackage(new FileInfo(datiImport.FilePath)))
            {
                var ws = p.Workbook.Worksheets.First();
                var riga = 1;
                if (datiImport.IsPrimaRigaIntestazione)
                {
                    riga++;
                }
                var properties = GetReadWriteProperties();

                for (int r = riga; r < ws.Dimension.Rows; r++)
                {
                    T model = (T)Activator.CreateInstance(typeof(T));
                    for (int c = 1; c < ws.Dimension.Columns; c++)
                    {
                        var propName = datiImport.Colonne[c - 1];
                        var item = ws.GetValue(r, c);
                        SetData(model, properties, propName, item);
                    }
                    Aggiungi(model);

                }
                await Salva();
            }
        }
    }
}
