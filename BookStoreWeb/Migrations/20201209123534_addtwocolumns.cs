using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStoreWeb.Migrations
{
    public partial class addtwocolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateOn",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateOn",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateOn",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UpdateOn",
                table: "Books");
        }
    }
}
