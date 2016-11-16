using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ADL.Models;

namespace ADL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161115140620_Changed Id")]
    partial class ChangedId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ADL.Models.Assignment", b =>
                {
                    b.Property<int>("AssignmentId")
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

                    b.HasKey("AssignmentId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("ADL.Models.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });
        }
    }
}
