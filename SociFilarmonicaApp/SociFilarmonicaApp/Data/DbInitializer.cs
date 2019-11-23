using SociFilarmonicaApp.Models;
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

            var soci = new Socio[]
            {
                new Socio { Nome = "Mario", Cognome = "Rossi", Telefono = "001 1234", Email = "example@example.com", TipologiaSocioID = 1 }
                ,new Socio { Nome = "Luigi", Cognome = "Rossi", Telefono = "001 1234", Email = "example@example.com", TipologiaSocioID = 1 }
                ,new Socio { Nome = "Carla", Cognome = "Bianchi", Telefono = "001 1234", Email = "example@example.com", TipologiaSocioID = 3 }
                ,new Socio { Nome = "Lucia", Cognome = "Mondella", Telefono = "001 1234", Email = "example@example.com", TipologiaSocioID = 1 }
                ,new Socio { Nome = "Lorenzo", Cognome = "De Medici", Telefono = "001 1234", Email = "example@example.com", TipologiaSocioID = 2 }
            };

            foreach (var item in soci)
            {
                context.Soci.Add(item);
            }
            context.SaveChanges();

        }
    }
}
