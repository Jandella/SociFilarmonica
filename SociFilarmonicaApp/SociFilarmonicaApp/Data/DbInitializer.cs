using SociFilarmonicaApp.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SociFilarmonicaApp.Data
{
    public static class DbInitializer
    {
        public static void Initialize(FilarmonicaContext context)
        {
            

            // Look for any tipo.
            if (context.TipologiaSoci.Any())
            {
                return;   // DB has been seeded
            }

            var tipi = new TipologiaSocio[]
            {
                new TipologiaSocio { Descrizione = "Suonatore" }
                , new TipologiaSocio { Descrizione = "Simpatizzante" }
                , new TipologiaSocio { Descrizione = "Aiutante" }
            };

            foreach (var item in tipi)
            {
                context.TipologiaSoci.Add(item);
            }
            context.SaveChanges();

            var auto = new InfoAuto[]
            {
                new InfoAuto {TipoAuto = "Sotto x ccc", Carburante = "Benzina", RimborsoKm = 0.47m }
                , new InfoAuto {TipoAuto = "Sotto x ccc", Carburante = "Diesel", RimborsoKm = 0.47m }
                , new InfoAuto {TipoAuto = "Tra x ccc e y ccc", Carburante = "Benzina", RimborsoKm = 0.47m }
            };

            foreach (var item in auto)
            {
                context.InfoAutomobili.Add(item);
            };
            context.SaveChanges();
#if DEBUG
            var soci = new Socio[]
            {
                new Socio { Nome = "Mario", Cognome = "Rossi", Telefono = "001 1234", Email = "example@example.com", TipologiaSocioID = 1, NumeroSocio = 1234 }
                ,new Socio { Nome = "Luigi", Cognome = "Rossi", Telefono = "001 1234", Email = "example@example.com", TipologiaSocioID = 1, NumeroSocio = 5678 }
                ,new Socio { Nome = "Carla", Cognome = "Bianchi", Telefono = "001 1234", Email = "example@example.com", TipologiaSocioID = 3, NumeroSocio = 9012 }
                ,new Socio { Nome = "Lucia", Cognome = "Mondella", Telefono = "001 1234", Email = "example@example.com", TipologiaSocioID = 1, NumeroSocio = 3456 }
                ,new Socio { Nome = "Lorenzo", Cognome = "De Medici", Telefono = "001 1234", Email = "example@example.com", TipologiaSocioID = 2, NumeroSocio = 7890 }
            };

            foreach (var item in soci)
            {
                context.Soci.Add(item);
            }
            context.SaveChanges();
#endif
        }
    }
}
