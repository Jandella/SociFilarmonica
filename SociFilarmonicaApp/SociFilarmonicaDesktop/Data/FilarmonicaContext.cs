using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SociFilarmonicaDesktop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SociFilarmonicaDesktop.Data
{
    public class FilarmonicaContext : DbContext
    {
        public FilarmonicaContext() : base()
        {

        }
        public FilarmonicaContext(DbContextOptions<FilarmonicaContext> options)
            : base(options)
        {
        }

        public DbSet<Socio> Soci { get; set; }
        public DbSet<TipologiaSocio> TipologiaSoci { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Socio>().ToTable("Socio");
            modelBuilder.Entity<TipologiaSocio>().ToTable("TipologiaSocio");
        }

        public class FilarmonicaContextFactory : IDesignTimeDbContextFactory<FilarmonicaContext>
        {
            public FilarmonicaContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<FilarmonicaContext>();
                optionsBuilder.UseSqlite("Data Source=blog.db");

                return new FilarmonicaContext(optionsBuilder.Options);
            }
        }
    }
}
