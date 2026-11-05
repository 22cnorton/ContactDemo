using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactDemo.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAddressId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_Id",
                table: "Contacts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Address_Id",
                table: "Contacts",
                type: "uuid",
                nullable: true);
        }
    }
}
