﻿// <auto-generated />
using AuditManagementPortalClientMVC.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AuditManagementPortalClientMVC.Migrations
{
    [DbContext(typeof(AuditDbContext))]
    [Migration("20210613111222_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AuditManagementPortalClientMVC.Models.StoreAuditResponse", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationOwnerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuditDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AuditId")
                        .HasColumnType("int");

                    b.Property<string>("AuditType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectExecutionStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectManagerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RemedialActionDuration")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("storeAuditResponses");
                });
#pragma warning restore 612, 618
        }
    }
}
