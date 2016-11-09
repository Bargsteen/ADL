using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ADL.Models;

namespace ADL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161109123841_Intial")]
    partial class Intial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("ADL.Models.Assignment", b =>
                {
                    b.Property<int>("AssignmentID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnswerOptionFour");

                    b.Property<string>("AnswerOptionOne")
                        .IsRequired();

                    b.Property<string>("AnswerOptionThree");

                    b.Property<string>("AnswerOptionTwo")
                        .IsRequired();

                    b.Property<int>("CorrectAnswer");

                    b.Property<string>("Headline")
                        .IsRequired();

                    b.Property<string>("Question")
                        .IsRequired();

                    b.HasKey("AssignmentID");

                    b.ToTable("Assignments");
                });
        }
    }
}
