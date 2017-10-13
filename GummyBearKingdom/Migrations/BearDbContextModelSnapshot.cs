using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using GummyBearKingdom.Models;

namespace GummyBearKingdom.Migrations
{
    [DbContext(typeof(BearDbContext))]
    partial class BearDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("GummyBearKingdom.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CountryOfOrigin");

                    b.Property<string>("Image");

                    b.Property<double>("ProductCost");

                    b.Property<string>("ProductName");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });
        }
    }
}
