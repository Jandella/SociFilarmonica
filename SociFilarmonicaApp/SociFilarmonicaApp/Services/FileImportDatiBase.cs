using Microsoft.Extensions.Logging;
using SociFilarmonicaApp.Data;
using SociFilarmonicaApp.Data.DbModels;
using SociFilarmonicaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Services
{
    public abstract class FileImportDatiBase<T> : IFileImportDati<T>
    {
        private FilarmonicaContext _context;
        protected ILogger _logger;
        public FileImportDatiBase(FilarmonicaContext ctx, ILogger logger)
        {
            _context = ctx;
            _logger = logger;
        }
        public abstract Task Import(ImportVM datiImport);

        //protected abstract IEnumerable<T> Leggi(ImportVM datiImport, int? fromRiga = null, int? toRiga = null);

        protected System.Reflection.PropertyInfo[] GetReadWriteProperties()
        {
            var properties = typeof(T).GetProperties()
                    .Where(x => x.CanRead && x.CanWrite).ToArray();
            return properties;
        }

        protected void SetData(T model, System.Reflection.PropertyInfo[] properties, string propName, object value)
        {
            var propObj = properties.FirstOrDefault(x => x.Name == propName);
            if(propObj == null)
            {
                _logger?.LogDebug("{propName} non è tra le proprietà dell'oggetto, skip.", propName);
                return;
            }
            //todo: prova a fare un covert

            var currentPropType = propObj.PropertyType;
            if(value == null)
            {
                _logger?.LogDebug("Il valore da assegnare a {propName} è NULL, skip.", propName);
            }
            else if(value is string && currentPropType != typeof(string))
            {
                object val = Activator.CreateInstance(currentPropType);
                try
                {
                    if (currentPropType == typeof(DateTime))
                    {
                        val = Convert.ToDateTime(value, System.Globalization.CultureInfo.CurrentCulture);
                    }
                    else if (currentPropType == typeof(int) || currentPropType == typeof(int?))
                    {
                        val = Convert.ToInt32(value);
                    }
                    else if(currentPropType == typeof(decimal) || currentPropType == typeof(decimal?))
                    {
                        val = Convert.ToDecimal(value, System.Globalization.CultureInfo.CurrentCulture);
                    }
                    else if(currentPropType == typeof(double) || currentPropType == typeof(double?))
                    {
                        val = Convert.ToDouble(value, System.Globalization.CultureInfo.CurrentCulture);
                    }
                }
                catch(FormatException ex)
                {
                    _logger?.LogWarning("FORMAT: valore che ha provato a convertire: {value}. Imposto default.", value);
                    _logger?.LogTrace(ex, "");
                }
                catch (InvalidCastException ex)
                {
                    _logger?.LogWarning("INVALID CAST: valore che ha provato a convertire: {value}. Imposto default", value);
                    _logger?.LogTrace(ex, "");
                }
                catch (OverflowException ex)
                {
                    _logger?.LogWarning("OVERFLOW: valore che ha provato a convertire: {value}. Imposto default", value);
                    _logger?.LogTrace(ex, "");
                }
                catch (Exception ex)
                {
                    _logger?.LogWarning("{msg}: valore che ha provato a convertire: {value}. Imposto default", value);
                    _logger?.LogTrace(ex, "");
                }
                propObj.SetValue(model, val);
            }
            else
            {
                propObj.SetValue(model, value);
            }
            
        }

        protected void Aggiungi(T model)
        {
            _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        }

        protected Task Salva()
        {
            return _context.SaveChangesAsync();
        }

    }
}
