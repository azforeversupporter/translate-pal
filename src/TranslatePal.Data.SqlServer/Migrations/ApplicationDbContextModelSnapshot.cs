using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TranslatePal.Data.SqlServer;

namespace TranslatePal.Data.SqlServer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TranslatePal.Data.SqlServer.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DefaultLanguage")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 7);

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Languages")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.HasKey("Id");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("TranslatePal.Data.SqlServer.Bundle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ApplicationId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.HasKey("Id");

                    b.HasAlternateKey("Name", "ApplicationId");

                    b.HasIndex("ApplicationId");

                    b.ToTable("Bundles");
                });

            modelBuilder.Entity("TranslatePal.Data.SqlServer.Element", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BundleId");

                    b.Property<string>("Comment");

                    b.Property<string>("ElementName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.HasKey("Id");

                    b.HasAlternateKey("BundleId", "ElementName");

                    b.HasIndex("BundleId");

                    b.ToTable("Elements");
                });

            modelBuilder.Entity("TranslatePal.Data.SqlServer.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<int>("ElementId");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 10);

                    b.Property<string>("Translation")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("ElementId", "Language");

                    b.HasIndex("ElementId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("TranslatePal.Data.SqlServer.Bundle", b =>
                {
                    b.HasOne("TranslatePal.Data.SqlServer.Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TranslatePal.Data.SqlServer.Element", b =>
                {
                    b.HasOne("TranslatePal.Data.SqlServer.Bundle")
                        .WithMany()
                        .HasForeignKey("BundleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TranslatePal.Data.SqlServer.Resource", b =>
                {
                    b.HasOne("TranslatePal.Data.SqlServer.Element")
                        .WithMany()
                        .HasForeignKey("ElementId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
