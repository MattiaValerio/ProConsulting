﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RapportiWeb.Server.Data;

#nullable disable

namespace RapportiWeb.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231006120632_fix_registration3")]
    partial class fix_registration3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RapportiWeb.Shared.Cliente", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("CAP")
                        .HasColumnType("int");

                    b.Property<string>("Citta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Indirizzo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeAbbreviato")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Provincia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Responsabile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Stato")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ragioneSociale")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Clienti");
                });

            modelBuilder.Entity("RapportiWeb.Shared.Fad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("CodRichiesta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodSessione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IncaricatoFad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RespRic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RifOfferta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Fad");
                });

            modelBuilder.Entity("RapportiWeb.Shared.Rapporto", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("Clienteid")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DataIntervento")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DurataIntervento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdCollegamento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Incaricato")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LuogoIntervento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RespInt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RespRic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RichiestaId")
                        .HasColumnType("int");

                    b.Property<string>("TipoIntervento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dataCreazioneRapporto")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("Clienteid");

                    b.ToTable("Rapporti");
                });

            modelBuilder.Entity("RapportiWeb.Shared.Richiesta", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("Clienteid")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataIntervento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DurataIntervento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FiguraProfessionale")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Firma")
                        .HasColumnType("bit");

                    b.Property<bool>("RapportoCreato")
                        .HasColumnType("bit");

                    b.Property<string>("ResponsabileRic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipologiaIntervento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("visible")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.HasIndex("Clienteid");

                    b.ToTable("Richieste");
                });

            modelBuilder.Entity("RapportiWeb.Shared.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amministratore")
                        .HasColumnType("int");

                    b.Property<int>("Attivo")
                        .HasColumnType("int");

                    b.Property<string>("Cognome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Figuraprof")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Hash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Organizzazioneid")
                        .HasColumnType("int");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("TipoUtente")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Organizzazioneid");

                    b.ToTable("Utenti");
                });

            modelBuilder.Entity("RapportiWeb.Shared.Rapporto", b =>
                {
                    b.HasOne("RapportiWeb.Shared.Cliente", null)
                        .WithMany("Rapporti")
                        .HasForeignKey("Clienteid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RapportiWeb.Shared.Richiesta", b =>
                {
                    b.HasOne("RapportiWeb.Shared.Cliente", null)
                        .WithMany("Richieste")
                        .HasForeignKey("Clienteid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RapportiWeb.Shared.User", b =>
                {
                    b.HasOne("RapportiWeb.Shared.Cliente", "Organizzazione")
                        .WithMany("Utenti")
                        .HasForeignKey("Organizzazioneid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizzazione");
                });

            modelBuilder.Entity("RapportiWeb.Shared.Cliente", b =>
                {
                    b.Navigation("Rapporti");

                    b.Navigation("Richieste");

                    b.Navigation("Utenti");
                });
#pragma warning restore 612, 618
        }
    }
}
