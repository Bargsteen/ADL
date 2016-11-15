using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ADL.Migrations
{
    public partial class UpdatedLocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Titel",
                table: "Locations");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Locations",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Locations");

            migrationBuilder.AddColumn<string>(
                name: "Titel",
                table: "Locations",
                nullable: false,
                defaultValue: "");
        }
    }
}
