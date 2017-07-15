// ReSharper disable UnusedMember.Global
// ReSharper disable PartialTypeWithSinglePart

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

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

            modelBuilder.Entity("DomainLayer.Models.Person", b =>
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

            modelBuilder.Entity("DomainLayer.Models.Product", b =>
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

            modelBuilder.Entity("DomainLayer.Models.ProductPerson", b =>
                {
                    b.Property<int>("PersonId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Role");

                    b.HasKey("PersonId", "ProductId", "Role");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductPerson");
                });

            modelBuilder.Entity("DomainLayer.Models.ProductPerson", b =>
                {
                    b.HasOne("DomainLayer.Models.Person", "Person")
                        .WithMany("ProductPerson")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DomainLayer.Models.Product", "Product")
                        .WithMany("ProductPerson")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}