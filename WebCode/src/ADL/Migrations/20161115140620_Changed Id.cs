using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ADL.Migrations
{
    public partial class ChangedId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LocationID",
                table: "Locations",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "AssignmentID",
                table: "Assignments",
                nullable: false)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.RenameColumn(
                name: "LocationID",
                table: "Locations",
                newName: "LocationId");

            migrationBuilder.RenameColumn(
                name: "AssignmentID",
                table: "Assignments",
                newName: "AssignmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Locations",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "AssignmentId",
                table: "Assignments",
                nullable: false);

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Locations",
                newName: "LocationID");

            migrationBuilder.RenameColumn(
                name: "AssignmentId",
                table: "Assignments",
                newName: "AssignmentID");
        }
    }
}
