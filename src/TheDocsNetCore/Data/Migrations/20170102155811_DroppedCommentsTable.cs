using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TheDocsNetCore.Data.Migrations
{
    public partial class DroppedCommentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Series_SeriesID",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "SeriesID",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Series_SeriesID",
                table: "Posts",
                column: "SeriesID",
                principalTable: "Series",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Series_SeriesID",
                table: "Posts");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Author = table.Column<string>(maxLength: 450, nullable: false),
                    Content = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    PostID = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_Author",
                        column: x => x.Author,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostID",
                        column: x => x.PostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AlterColumn<int>(
                name: "SeriesID",
                table: "Posts",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Author",
                table: "Comments",
                column: "Author");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostID",
                table: "Comments",
                column: "PostID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Series_SeriesID",
                table: "Posts",
                column: "SeriesID",
                principalTable: "Series",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
