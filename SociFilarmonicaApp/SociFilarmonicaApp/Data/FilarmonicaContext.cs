using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SociFilarmonicaApp.Models;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Socio>().ToTable("Socio");
            modelBuilder.Entity<TipologiaSocio>().ToTable("TipologiaSocio");
        }
    }
}
