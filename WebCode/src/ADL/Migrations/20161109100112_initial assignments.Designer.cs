using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ADL.Models;

namespace ADL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161109100112_initial assignments")]
    partial class initialassignments
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("ADL.Models.AnswerOption", b =>
                {
                    b.Property<int>("AnswerOptionID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AssignmentID");

                    b.Property<string>("Text");

                    b.HasKey("AnswerOptionID");

                    b.HasIndex("AssignmentID");

                    b.ToTable("AnswerOptions");
                });

            modelBuilder.Entity("ADL.Models.Assignment", b =>
                {
                    b.Property<int>("AssignmentID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CorrectAnswer");

                    b.Property<string>("Headline")
                        .IsRequired();

                    b.Property<string>("Question")
                        .IsRequired();

                    b.HasKey("AssignmentID");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("ADL.Models.AnswerOption", b =>
                {
                    b.HasOne("ADL.Models.Assignment")
                        .WithMany("AnswerOptions")
                        .HasForeignKey("AssignmentID");
                });
        }
    }
}
