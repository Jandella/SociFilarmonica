﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SociFilarmonicaApp.Data;

namespace SociFilarmonicaApp.Migrations
{
    [DbContext(typeof(FilarmonicaContext))]
    partial class FilarmonicaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8");

            modelBuilder.Entity("SociFilarmonicaApp.Data.DbModels.InfoAuto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Carburante")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataCreazione")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataUltimaModifica")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("RimborsoKm")
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoAuto")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("InfoAuto");
                });

            modelBuilder.Entity("SociFilarmonicaApp.Data.DbModels.Quote", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Anno")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataCreazione")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataUltimaModifica")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("QuotaSociale")
                        .HasColumnType("TEXT");

                    b.Property<int>("SocioID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("SocioID");

                    b.ToTable("Quote");
                });

            modelBuilder.Entity("SociFilarmonicaApp.Data.DbModels.RimborsoKm", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataCreazione")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataUltimaModifica")
                        .HasColumnType("TEXT");

                    b.Property<string>("DatiRimborsoSerializzati")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descrizione")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<int>("SocioID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("SocioID");

                    b.ToTable("Rimborsi");
                });

            modelBuilder.Entity("SociFilarmonicaApp.Data.DbModels.Socio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Annullato")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cap")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Citta")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("CodiceFiscale")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Cognome")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<DateTime>("DataCreazione")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DataNascita")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataUltimaModifica")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DatiAutoID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DescrizioneMacchina")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<string>("Email2")
                        .HasColumnType("TEXT");

                    b.Property<string>("Indirizzo")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("LuogoNascita")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<int>("NumeroSocio")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NumeroTesseraAmbima")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<bool>("PrivacyFirmata")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TargaMacchina")
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("Telefono")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("Telefono2")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<int?>("TipologiaSocioID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("DatiAutoID");

                    b.HasIndex("TipologiaSocioID");

                    b.ToTable("Socio");
                });

            modelBuilder.Entity("SociFilarmonicaApp.Data.DbModels.TipologiaSocio", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataCreazione")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataUltimaModifica")
                        .HasColumnType("TEXT");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("TipologiaSocio");
                });

            modelBuilder.Entity("SociFilarmonicaApp.Data.DbModels.Quote", b =>
                {
                    b.HasOne("SociFilarmonicaApp.Data.DbModels.Socio", "Socio")
                        .WithMany("RegistroQuote")
                        .HasForeignKey("SocioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SociFilarmonicaApp.Data.DbModels.RimborsoKm", b =>
                {
                    b.HasOne("SociFilarmonicaApp.Data.DbModels.Socio", "Socio")
                        .WithMany()
                        .HasForeignKey("SocioID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SociFilarmonicaApp.Data.DbModels.Socio", b =>
                {
                    b.HasOne("SociFilarmonicaApp.Data.DbModels.InfoAuto", "DatiAuto")
                        .WithMany()
                        .HasForeignKey("DatiAutoID");

                    b.HasOne("SociFilarmonicaApp.Data.DbModels.TipologiaSocio", "Tipologia")
                        .WithMany("SociDiCategoria")
                        .HasForeignKey("TipologiaSocioID");
                });
#pragma warning restore 612, 618
        }
    }
}
