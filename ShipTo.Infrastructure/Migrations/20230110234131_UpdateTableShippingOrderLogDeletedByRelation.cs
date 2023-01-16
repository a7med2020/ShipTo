using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipTo.Infrastructure.Migrations
{
    public partial class UpdateTableShippingOrderLogDeletedByRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "ShippingOrderLogs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShippingOrderLogs_DeletedBy",
                table: "ShippingOrderLogs",
                column: "DeletedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingOrderLogs_Users_DeletedBy",
                table: "ShippingOrderLogs",
                column: "DeletedBy",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingOrderLogs_Users_DeletedBy",
                table: "ShippingOrderLogs");

            migrationBuilder.DropIndex(
                name: "IX_ShippingOrderLogs_DeletedBy",
                table: "ShippingOrderLogs");

            migrationBuilder.AlterColumn<string>(
                name: "DeletedBy",
                table: "ShippingOrderLogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
