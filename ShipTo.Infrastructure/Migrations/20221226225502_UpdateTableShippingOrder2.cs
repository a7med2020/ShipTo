using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipTo.Infrastructure.Migrations
{
    public partial class UpdateTableShippingOrder2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ShippingOrders_DeliveryStatusId",
                table: "ShippingOrders",
                column: "DeliveryStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingOrders_DeliveryStatuses_DeliveryStatusId",
                table: "ShippingOrders",
                column: "DeliveryStatusId",
                principalTable: "DeliveryStatuses",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingOrders_DeliveryStatuses_DeliveryStatusId",
                table: "ShippingOrders");

            migrationBuilder.DropIndex(
                name: "IX_ShippingOrders_DeliveryStatusId",
                table: "ShippingOrders");
        }
    }
}
