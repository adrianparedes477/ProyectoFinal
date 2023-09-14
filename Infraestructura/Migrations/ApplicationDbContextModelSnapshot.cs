﻿// <auto-generated />
using System;
using Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infraestructura.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Core.Entidades.Proyecto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codProyecto");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("Proyecto");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Direccion = "Calle falsa 123",
                            Estado = 2,
                            Nombre = "Proyecto A"
                        },
                        new
                        {
                            Id = 2,
                            Direccion = "Calle falsa 456",
                            Estado = 3,
                            Nombre = "Proyecto B"
                        },
                        new
                        {
                            Id = 3,
                            Direccion = "Calle falsa 789",
                            Estado = 1,
                            Nombre = "Proyecto C"
                        });
                });

            modelBuilder.Entity("Core.Entidades.Servicio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codServicio");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descr")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<decimal>("ValorHora")
                        .HasMaxLength(60)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Servicio");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descr = "Servicio 1",
                            Estado = true,
                            ValorHora = 10.50m
                        },
                        new
                        {
                            Id = 2,
                            Descr = "Servicio 2",
                            Estado = false,
                            ValorHora = 15.75m
                        });
                });

            modelBuilder.Entity("Core.Entidades.Trabajo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codTrabajo");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CantHoras")
                        .HasMaxLength(60)
                        .HasColumnType("int");

                    b.Property<int>("CodProyecto")
                        .HasColumnType("int");

                    b.Property<int>("CodServicio")
                        .HasColumnType("int");

                    b.Property<decimal>("Costo")
                        .HasMaxLength(60)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("ValorHora")
                        .HasMaxLength(60)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CodProyecto");

                    b.HasIndex("CodServicio");

                    b.ToTable("Trabajo");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CantHoras = 20,
                            CodProyecto = 1,
                            CodServicio = 1,
                            Costo = 20m,
                            Fecha = new DateTime(2023, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ValorHora = 10m
                        });
                });

            modelBuilder.Entity("Core.Entidades.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("codUsuario");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Contrasenia")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("Dni")
                        .HasMaxLength(60)
                        .HasColumnType("int");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("Tipo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(2);

                    b.HasKey("Id");

                    b.ToTable("Usuario");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Contrasenia = "$2a$10$tY1kk65X7.wUWHuK2ezLkuMxjrCbhKOD9rcRmGWooYifOvQbtp2kW",
                            Dni = 12345678,
                            NombreCompleto = "Admin",
                            Tipo = 1
                        },
                        new
                        {
                            Id = 2,
                            Contrasenia = "$2a$10$Is8PoY3MdfPQyMOc50SSjOwelIh0s4zX2S34MLeOb6KsQvT1DnQDe",
                            Dni = 87654321,
                            NombreCompleto = "Consultor",
                            Tipo = 2
                        });
                });

            modelBuilder.Entity("Core.Entidades.Trabajo", b =>
                {
                    b.HasOne("Core.Entidades.Proyecto", "Proyecto")
                        .WithMany()
                        .HasForeignKey("CodProyecto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entidades.Servicio", "Servicio")
                        .WithMany()
                        .HasForeignKey("CodServicio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proyecto");

                    b.Navigation("Servicio");
                });
#pragma warning restore 612, 618
        }
    }
}
