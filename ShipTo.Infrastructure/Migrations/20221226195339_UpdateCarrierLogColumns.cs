using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipTo.Infrastructure.Migrations
{
    public partial class UpdateCarrierLogColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Carriers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Carriers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Carriers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Carriers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Carriers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModefiedBy",
                table: "Carriers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModefiedDate",
                table: "Carriers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Carriers",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carriers_CreatedBy",
                table: "Carriers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Carriers_ModefiedBy",
                table: "Carriers",
                column: "ModefiedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Carriers_Users_CreatedBy",
                table: "Carriers",
                column: "CreatedBy",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Carriers_Users_ModefiedBy",
                table: "Carriers",
                column: "ModefiedBy",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carriers_Users_CreatedBy",
                table: "Carriers");

            migrationBuilder.DropForeignKey(
                name: "FK_Carriers_Users_ModefiedBy",
                table: "Carriers");

            migrationBuilder.DropIndex(
                name: "IX_Carriers_CreatedBy",
                table: "Carriers");

            migrationBuilder.DropIndex(
                name: "IX_Carriers_ModefiedBy",
                table: "Carriers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Carriers");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Carriers");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Carriers");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Carriers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Carriers");

            migrationBuilder.DropColumn(
                name: "ModefiedBy",
                table: "Carriers");

            migrationBuilder.DropColumn(
                name: "ModefiedDate",
                table: "Carriers");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Carriers");
        }
    }
}
