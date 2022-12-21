using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipTo.Infrastructure.Migrations
{
    public partial class UpdateTableShippersAddUniquenesstoColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Shippers_Email",
                table: "Shippers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Shippers_Name",
                table: "Shippers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shippers_Phone",
                table: "Shippers",
                column: "Phone",
                unique: true,
                filter: "[Phone] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shippers_Email",
                table: "Shippers");

            migrationBuilder.DropIndex(
                name: "IX_Shippers_Name",
                table: "Shippers");

            migrationBuilder.DropIndex(
                name: "IX_Shippers_Phone",
                table: "Shippers");
        }
    }
}
