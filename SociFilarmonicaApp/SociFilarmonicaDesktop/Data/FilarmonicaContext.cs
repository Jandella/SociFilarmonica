using Microsoft.EntityFrameworkCore;
using SociFilarmonicaDesktop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SociFilarmonicaDesktop.Data
{
    public class FilarmonicaContext : DbContext
    {
        public FilarmonicaContext(DbContextOptions<FilarmonicaContext> options)
            : base(options)
        {
        }

        public DbSet<Socio> Soci { get; set; }
        public DbSet<TipologiaSocio> TipologiaSoci { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Socio>().ToTable("Socio");
            modelBuilder.Entity<TipologiaSocio>().ToTable("TipologiaSocio");
        }
    }
}
