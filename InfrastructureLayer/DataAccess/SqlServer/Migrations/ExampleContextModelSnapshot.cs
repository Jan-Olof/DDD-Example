using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using InfrastructureLayer.DataAccess.SqlServer;
using DomainLayer.Enums;

namespace InfrastructureLayer.DataAccess.SqlServer.Migrations
{
    [DbContext(typeof(ExampleContext))]
    partial class ExampleContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InfrastructureLayer.DataAccess.Daos.PersonDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("InfrastructureLayer.DataAccess.Daos.ProductDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("InfrastructureLayer.DataAccess.Daos.ProductPerson", b =>
                {
                    b.Property<int>("PersonId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Role");

                    b.HasKey("PersonId", "ProductId", "Role");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductPersons");
                });

            modelBuilder.Entity("InfrastructureLayer.DataAccess.Daos.ProductPerson", b =>
                {
                    b.HasOne("InfrastructureLayer.DataAccess.Daos.PersonDao", "Person")
                        .WithMany("ProductPersons")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InfrastructureLayer.DataAccess.Daos.ProductDao", "Product")
                        .WithMany("ProductPersons")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
