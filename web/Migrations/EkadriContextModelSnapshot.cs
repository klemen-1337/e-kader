﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using web.Data;

namespace web.Migrations
{
    [DbContext(typeof(EkadriContext))]
    partial class EkadriContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("web.Models.DelovnaMesta", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("IzobrazevanjeID")
                        .HasColumnType("int");

                    b.Property<string>("Lokacija")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NazivDelovnegaMesta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Oddelek")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ZaposlenID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IzobrazevanjeID");

                    b.HasIndex("ZaposlenID");

                    b.ToTable("DelovnaMesta");
                });

            modelBuilder.Entity("web.Models.DelovneUre", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UraKonca")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UraZacetka")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("DelovneUre");
                });

            modelBuilder.Entity("web.Models.Dopust", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("Preostanek")
                        .HasColumnType("int");

                    b.Property<DateTime>("UraKonca")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UraZacetka")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Dopusti");
                });

            modelBuilder.Entity("web.Models.Izobrazevanje", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cena")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Redno")
                        .HasColumnType("bit");

                    b.Property<bool>("Specializirano")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("Izobrazevanja");
                });

            modelBuilder.Entity("web.Models.Zaposlen", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumRojstva")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumZaposlitve")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DelovnaMestaID")
                        .HasColumnType("int");

                    b.Property<int?>("DelovneUreID")
                        .HasColumnType("int");

                    b.Property<int?>("DopustID")
                        .HasColumnType("int");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naslov")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Priimek")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Spol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefon")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DelovnaMestaID");

                    b.HasIndex("DelovneUreID");

                    b.HasIndex("DopustID");

                    b.ToTable("Zaposleni");
                });

            modelBuilder.Entity("web.Models.DelovnaMesta", b =>
                {
                    b.HasOne("web.Models.Izobrazevanje", null)
                        .WithMany("DelovnaMesta")
                        .HasForeignKey("IzobrazevanjeID");

                    b.HasOne("web.Models.Zaposlen", "Zaposlen")
                        .WithMany()
                        .HasForeignKey("ZaposlenID");
                });

            modelBuilder.Entity("web.Models.Zaposlen", b =>
                {
                    b.HasOne("web.Models.DelovnaMesta", null)
                        .WithMany("Zaposleni")
                        .HasForeignKey("DelovnaMestaID");

                    b.HasOne("web.Models.DelovneUre", null)
                        .WithMany("Zaposleni")
                        .HasForeignKey("DelovneUreID");

                    b.HasOne("web.Models.Dopust", null)
                        .WithMany("Zaposleni")
                        .HasForeignKey("DopustID");
                });
#pragma warning restore 612, 618
        }
    }
}
