using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ADL.Migrations
{
    public partial class Intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    AssignmentID = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    AnswerOptionFour = table.Column<string>(nullable: true),
                    AnswerOptionOne = table.Column<string>(nullable: false),
                    AnswerOptionThree = table.Column<string>(nullable: true),
                    AnswerOptionTwo = table.Column<string>(nullable: false),
                    CorrectAnswer = table.Column<int>(nullable: false),
                    Headline = table.Column<string>(nullable: false),
                    Question = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.AssignmentID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignments");
        }
    }
}
