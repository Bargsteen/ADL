﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ADL.Models;

namespace ADL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161120183051_Answers2")]
    partial class Answers2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("ADL.Models.Answer", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnsweredAssignmentAssignmentId");

                    b.Property<int?>("ChosenAnswerOptionAnswerOptionID");

                    b.Property<DateTime>("TimeAnswered");

                    b.HasKey("AnswerId");

                    b.HasIndex("AnsweredAssignmentAssignmentId");

                    b.HasIndex("ChosenAnswerOptionAnswerOptionID");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("ADL.Models.AnswerOption", b =>
                {
                    b.Property<int>("AnswerOptionID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AssignmentId");

                    b.Property<string>("Text");

                    b.HasKey("AnswerOptionID");

                    b.HasIndex("AssignmentId");

                    b.ToTable("AnswerOptions");
                });

            modelBuilder.Entity("ADL.Models.Assignment", b =>
                {
                    b.Property<int>("AssignmentId")
                        .ValueGeneratedOnAdd();

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

                    b.Property<int>("AttachedAssignmentId");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("ADL.Models.Answer", b =>
                {
                    b.HasOne("ADL.Models.Assignment", "AnsweredAssignment")
                        .WithMany()
                        .HasForeignKey("AnsweredAssignmentAssignmentId");

                    b.HasOne("ADL.Models.AnswerOption", "ChosenAnswerOption")
                        .WithMany()
                        .HasForeignKey("ChosenAnswerOptionAnswerOptionID");
                });

            modelBuilder.Entity("ADL.Models.AnswerOption", b =>
                {
                    b.HasOne("ADL.Models.Assignment")
                        .WithMany("AnswerOptions")
                        .HasForeignKey("AssignmentId");
                });
        }
    }
}
