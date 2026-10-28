using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactDemo.Migrations
{
    /// <inheritdoc />
    public partial class Birthday : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "Birthday",
                table: "Contacts",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Contacts");
        }
    }
}
