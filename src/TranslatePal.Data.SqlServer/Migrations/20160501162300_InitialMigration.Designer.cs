using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using TranslatePal.Data.SqlServer;

namespace TranslatePal.Data.SqlServer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160501162300_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
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
                });

            modelBuilder.Entity("TranslatePal.Data.SqlServer.Bundle", b =>
                {
                    b.HasOne("TranslatePal.Data.SqlServer.Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("TranslatePal.Data.SqlServer.Element", b =>
                {
                    b.HasOne("TranslatePal.Data.SqlServer.Bundle")
                        .WithMany()
                        .HasForeignKey("BundleId");
                });

            modelBuilder.Entity("TranslatePal.Data.SqlServer.Resource", b =>
                {
                    b.HasOne("TranslatePal.Data.SqlServer.Element")
                        .WithMany()
                        .HasForeignKey("ElementId");
                });
        }
    }
}
