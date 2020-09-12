using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Data.DbModels;

namespace SociFilarmonicaApp.Data
{
    public class FilarmonicaContext : DbContext
    {
        public FilarmonicaContext (DbContextOptions<FilarmonicaContext> options)
            : base(options)
        {
        }

        public DbSet<Socio> Soci { get; set; }
        public DbSet<TipologiaSocio> TipologiaSoci { get; set; }
        public DbSet<InfoAuto> InfoAutomobili { get; set; }
        public DbSet<RimborsoKm> RimborsoKm { get; set; }
        public DbSet<Quote> QuoteAssociative { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Socio>().ToTable("Socio");
            modelBuilder.Entity<TipologiaSocio>().ToTable("TipologiaSocio");
            modelBuilder.Entity<InfoAuto>().ToTable("InfoAuto");
            modelBuilder.Entity<RimborsoKm>().ToTable("Rimborsi");
            modelBuilder.Entity<Quote>().ToTable("Quote");
        }

        
    }
}
