using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipTo.Infrastructure.Migrations
{
    public partial class UpdateTblShippingOrderDataannotation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
               name: "OrderNumber",
               table: "ShippingOrders",
               type: "nvarchar(30)",
               maxLength: 30,
               nullable: false,
               defaultValue: null,
               oldClrType: typeof(string),
               oldType: "nvarchar(30)",
               oldMaxLength: 30,
               oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderDetails",
                table: "ShippingOrders",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: null,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BulkId",
                table: "ShippingOrders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: null,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "ShippingOrders",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: null,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
