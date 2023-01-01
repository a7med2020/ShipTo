using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipTo.Infrastructure.Migrations
{
    public partial class UpdateShippingOrderCarrierIdNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingOrderLogs_Carriers_CarrierId",
                table: "ShippingOrderLogs");

            migrationBuilder.AlterColumn<int>(
                name: "CarrierId",
                table: "ShippingOrders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CarrierId",
                table: "ShippingOrderLogs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingOrderLogs_Carriers_CarrierId",
                table: "ShippingOrderLogs",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingOrderLogs_Carriers_CarrierId",
                table: "ShippingOrderLogs");

            migrationBuilder.AlterColumn<int>(
                name: "CarrierId",
                table: "ShippingOrders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarrierId",
                table: "ShippingOrderLogs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingOrderLogs_Carriers_CarrierId",
                table: "ShippingOrderLogs",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
