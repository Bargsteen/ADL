using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ADL.Migrations
{
    public partial class Answers2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerId = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    AnsweredAssignmentAssignmentId = table.Column<int>(nullable: true),
                    ChosenAnswerOptionAnswerOptionID = table.Column<int>(nullable: true),
                    TimeAnswered = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_Assignments_AnsweredAssignmentAssignmentId",
                        column: x => x.AnsweredAssignmentAssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "AssignmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answers_AnswerOptions_ChosenAnswerOptionAnswerOptionID",
                        column: x => x.ChosenAnswerOptionAnswerOptionID,
                        principalTable: "AnswerOptions",
                        principalColumn: "AnswerOptionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_AnsweredAssignmentAssignmentId",
                table: "Answers",
                column: "AnsweredAssignmentAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_ChosenAnswerOptionAnswerOptionID",
                table: "Answers",
                column: "ChosenAnswerOptionAnswerOptionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");
        }
    }
}
