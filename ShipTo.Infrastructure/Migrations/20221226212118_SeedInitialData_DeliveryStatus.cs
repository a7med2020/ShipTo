using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipTo.Infrastructure.Migrations
{
    public partial class SeedInitialData_DeliveryStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DeliveryStatuses",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { "UnderDelivery", "قيد التسليم" },
                    { "Delivered", "استلم" },
                    { "Refused", "رفض الاستلام" },
                    { "PartialDelivery", "تسليم جزئي" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeliveryStatuses",
                keyColumn: "ID",
                keyValue: "Delivered");

            migrationBuilder.DeleteData(
                table: "DeliveryStatuses",
                keyColumn: "ID",
                keyValue: "PartialDelivery");

            migrationBuilder.DeleteData(
                table: "DeliveryStatuses",
                keyColumn: "ID",
                keyValue: "Refused");

            migrationBuilder.DeleteData(
                table: "DeliveryStatuses",
                keyColumn: "ID",
                keyValue: "UnderDelivery");
        }
    }
}
