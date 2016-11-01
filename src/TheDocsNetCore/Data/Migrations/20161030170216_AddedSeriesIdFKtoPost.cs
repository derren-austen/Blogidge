using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheDocsNetCore.Data.Migrations
{
    public partial class AddedSeriesIdFKtoPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Series",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "SeriesID",
                table: "Posts",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_SeriesID",
                table: "Posts",
                column: "SeriesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Series_SeriesID",
                table: "Posts",
                column: "SeriesID",
                principalTable: "Series",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Series_SeriesID",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_SeriesID",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "SeriesID",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "Series",
                table: "Posts",
                maxLength: 25,
                nullable: true);
        }
    }
}
