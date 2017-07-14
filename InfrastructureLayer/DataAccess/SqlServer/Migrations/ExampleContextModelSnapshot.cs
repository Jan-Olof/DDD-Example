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
        }
    }
}