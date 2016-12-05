using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ADL.Models;

namespace ADL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("ADL.Models.AnswerOption", b =>
                {
                    b.Property<int>("AnswerOptionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ExclusiveChoiceAssignmentAssignmentId");

                    b.Property<int?>("MultipleChoiceAssignmentAssignmentId");

                    b.Property<string>("Text");

                    b.HasKey("AnswerOptionId");

                    b.HasIndex("ExclusiveChoiceAssignmentAssignmentId");

                    b.HasIndex("MultipleChoiceAssignmentAssignmentId");

                    b.ToTable("AnswerOptions");
                });

            modelBuilder.Entity("ADL.Models.Answers.Answer", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnsweredAssignmentId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<DateTime>("TimeAnswered");

                    b.Property<int>("Type");

                    b.Property<string>("UserId");

                    b.HasKey("AnswerId");

                    b.ToTable("Answers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Answer");
                });

            modelBuilder.Entity("ADL.Models.Answers.AnswerBool", b =>
                {
                    b.Property<int>("AnswerBoolId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MultipleChoiceAnswerAnswerId");

                    b.Property<int?>("MultipleChoiceAssignmentAssignmentId");

                    b.Property<bool>("Value");

                    b.HasKey("AnswerBoolId");

                    b.HasIndex("MultipleChoiceAnswerAnswerId");

                    b.HasIndex("MultipleChoiceAssignmentAssignmentId");

                    b.ToTable("AnswerBools");
                });

            modelBuilder.Entity("ADL.Models.Assignments.Assignment", b =>
                {
                    b.Property<int>("AssignmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AssignmentSetId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.HasKey("AssignmentId");

                    b.HasIndex("AssignmentSetId");

                    b.ToTable("Assignments");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Assignment");
                });

            modelBuilder.Entity("ADL.Models.AssignmentSet", b =>
                {
                    b.Property<int>("AssignmentSetId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatorId")
                        .IsRequired();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("LastUpdateDate");

                    b.Property<int>("PublicityLevel");

                    b.Property<int>("SchoolId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("AssignmentSetId");

                    b.ToTable("AssignmentSets");
                });

            modelBuilder.Entity("ADL.Models.Class", b =>
                {
                    b.Property<int>("ClassId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("SchoolId");

                    b.Property<int>("StartYear");

                    b.HasKey("ClassId");

                    b.ToTable("Classes");
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

            modelBuilder.Entity("ADL.Models.Person", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<int?>("ClassId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Firstname");

                    b.Property<string>("Lastname");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<int>("PersonType");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int?>("SchoolId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("SchoolId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ADL.Models.PersonAssignmentCoupling", b =>
                {
                    b.Property<int>("PersonAssignmentCouplingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AssignmentId");

                    b.Property<int?>("LocationId");

                    b.Property<string>("PersonId")
                        .IsRequired();

                    b.HasKey("PersonAssignmentCouplingId");

                    b.HasIndex("LocationId");

                    b.ToTable("PersonAssignmentCoupling");
                });

            modelBuilder.Entity("ADL.Models.School", b =>
                {
                    b.Property<int>("SchoolId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("InstitutionNumber");

                    b.Property<string>("SchoolName");

                    b.HasKey("SchoolId");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ADL.Models.Answers.ExclusiveChoiceAnswer", b =>
                {
                    b.HasBaseType("ADL.Models.Answers.Answer");

                    b.Property<int>("ChosenAnswer");

                    b.ToTable("ExclusiveChoiceAnswer");

                    b.HasDiscriminator().HasValue("ExclusiveChoiceAnswer");
                });

            modelBuilder.Entity("ADL.Models.Answers.MultipleChoiceAnswer", b =>
                {
                    b.HasBaseType("ADL.Models.Answers.Answer");


                    b.ToTable("MultipleChoiceAnswer");

                    b.HasDiscriminator().HasValue("MultipleChoiceAnswer");
                });

            modelBuilder.Entity("ADL.Models.Answers.TextAnswer", b =>
                {
                    b.HasBaseType("ADL.Models.Answers.Answer");

                    b.Property<string>("Text");

                    b.ToTable("TextAnswer");

                    b.HasDiscriminator().HasValue("TextAnswer");
                });

            modelBuilder.Entity("ADL.Models.Assignments.ExclusiveChoiceAssignment", b =>
                {
                    b.HasBaseType("ADL.Models.Assignments.Assignment");

                    b.Property<int>("CorrectAnswer");

                    b.ToTable("ExclusiveChoiceAssignment");

                    b.HasDiscriminator().HasValue("ExclusiveChoiceAssignment");
                });

            modelBuilder.Entity("ADL.Models.Assignments.MultipleChoiceAssignment", b =>
                {
                    b.HasBaseType("ADL.Models.Assignments.Assignment");


                    b.ToTable("MultipleChoiceAssignment");

                    b.HasDiscriminator().HasValue("MultipleChoiceAssignment");
                });

            modelBuilder.Entity("ADL.Models.Assignments.TextAssignment", b =>
                {
                    b.HasBaseType("ADL.Models.Assignments.Assignment");


                    b.ToTable("TextAssignment");

                    b.HasDiscriminator().HasValue("TextAssignment");
                });

            modelBuilder.Entity("ADL.Models.AnswerOption", b =>
                {
                    b.HasOne("ADL.Models.Assignments.ExclusiveChoiceAssignment")
                        .WithMany("AnswerOptions")
                        .HasForeignKey("ExclusiveChoiceAssignmentAssignmentId");

                    b.HasOne("ADL.Models.Assignments.MultipleChoiceAssignment")
                        .WithMany("AnswerOptions")
                        .HasForeignKey("MultipleChoiceAssignmentAssignmentId");
                });

            modelBuilder.Entity("ADL.Models.Answers.AnswerBool", b =>
                {
                    b.HasOne("ADL.Models.Answers.MultipleChoiceAnswer")
                        .WithMany("ChosenAnswers")
                        .HasForeignKey("MultipleChoiceAnswerAnswerId");

                    b.HasOne("ADL.Models.Assignments.MultipleChoiceAssignment")
                        .WithMany("AnswerCorrectness")
                        .HasForeignKey("MultipleChoiceAssignmentAssignmentId");
                });

            modelBuilder.Entity("ADL.Models.Assignments.Assignment", b =>
                {
                    b.HasOne("ADL.Models.AssignmentSet")
                        .WithMany("Assignments")
                        .HasForeignKey("AssignmentSetId");
                });

            modelBuilder.Entity("ADL.Models.Person", b =>
                {
                    b.HasOne("ADL.Models.Class")
                        .WithMany("People")
                        .HasForeignKey("ClassId");

                    b.HasOne("ADL.Models.School", "School")
                        .WithMany()
                        .HasForeignKey("SchoolId");
                });

            modelBuilder.Entity("ADL.Models.PersonAssignmentCoupling", b =>
                {
                    b.HasOne("ADL.Models.Location")
                        .WithMany("PersonAssignmentCouplings")
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ADL.Models.Person")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ADL.Models.Person")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ADL.Models.Person")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
