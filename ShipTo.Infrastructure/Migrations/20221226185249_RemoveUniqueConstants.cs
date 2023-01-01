using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipTo.Infrastructure.Migrations
{
    public partial class RemoveUniqueConstants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shippers_Email_IsDeleted",
                table: "Shippers");

            migrationBuilder.DropIndex(
                name: "IX_Shippers_Name_IsDeleted",
                table: "Shippers");

            migrationBuilder.DropIndex(
                name: "IX_Shippers_Phone_IsDeleted",
                table: "Shippers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Shippers_Email_IsDeleted",
                table: "Shippers",
                columns: new[] { "Email", "IsDeleted" },
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Shippers_Name_IsDeleted",
                table: "Shippers",
                columns: new[] { "Name", "IsDeleted" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shippers_Phone_IsDeleted",
                table: "Shippers",
                columns: new[] { "Phone", "IsDeleted" },
                unique: true,
                filter: "[Phone] IS NOT NULL");
        }
    }
}
