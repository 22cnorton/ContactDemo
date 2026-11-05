using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddressInline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Address_AddressId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_AddressId",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Contacts",
                newName: "Address_Id");

            migrationBuilder.AddColumn<string>(
                name: "Address_Line",
                table: "Contacts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Line2",
                table: "Contacts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_State",
                table: "Contacts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Town",
                table: "Contacts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Zip",
                table: "Contacts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_Line",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Address_Line2",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Address_State",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Address_Town",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Address_Zip",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "Address_Id",
                table: "Contacts",
                newName: "AddressId");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Line = table.Column<string>(type: "text", nullable: false),
                    Line2 = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<string>(type: "text", nullable: false),
                    Town = table.Column<string>(type: "text", nullable: false),
                    Zip = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_AddressId",
                table: "Contacts",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Address_AddressId",
                table: "Contacts",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");
        }
    }
}
