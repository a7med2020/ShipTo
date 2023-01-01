using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipTo.Infrastructure.Migrations
{
    public partial class UpdateTableShippingOrder1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingOrders_Carriers_CarrierId",
                table: "ShippingOrders");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ShippingOrders_DeliveryStatuses_DeliveryStatusId",
            //    table: "ShippingOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingOrders_Shippers_ShipperId",
                table: "ShippingOrders");

            migrationBuilder.DropIndex(
                name: "IX_ShippingOrders_DeliveryStatusId",
                table: "ShippingOrders");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "ShippingOrders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "shippingOrderLogs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingOrderID = table.Column<int>(type: "int", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ClientPhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Governorate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ShipperId = table.Column<int>(type: "int", nullable: false),
                    OrderDetails = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    OrderPiecesCount = table.Column<int>(type: "int", nullable: true),
                    OrderTotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderNetPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryStatusId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DeliveryStatusReason = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarrierId = table.Column<int>(type: "int", nullable: false),
                    FileDataName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ModefiedBy = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModefiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shippingOrderLogs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_shippingOrderLogs_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shippingOrderLogs_DeliveryStatuses_DeliveryStatusId",
                        column: x => x.DeliveryStatusId,
                        principalTable: "DeliveryStatuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shippingOrderLogs_Shippers_ShipperId",
                        column: x => x.ShipperId,
                        principalTable: "Shippers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shippingOrderLogs_ShippingOrders_ShippingOrderID",
                        column: x => x.ShippingOrderID,
                        principalTable: "ShippingOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shippingOrderLogs_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_shippingOrderLogs_Users_ModefiedBy",
                        column: x => x.ModefiedBy,
                        principalSchema: "security",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_shippingOrderLogs_CarrierId",
                table: "shippingOrderLogs",
                column: "CarrierId");

            migrationBuilder.CreateIndex(
                name: "IX_shippingOrderLogs_CreatedBy",
                table: "shippingOrderLogs",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_shippingOrderLogs_DeliveryStatusId",
                table: "shippingOrderLogs",
                column: "DeliveryStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_shippingOrderLogs_ModefiedBy",
                table: "shippingOrderLogs",
                column: "ModefiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_shippingOrderLogs_ShipperId",
                table: "shippingOrderLogs",
                column: "ShipperId");

            migrationBuilder.CreateIndex(
                name: "IX_shippingOrderLogs_ShippingOrderID",
                table: "shippingOrderLogs",
                column: "ShippingOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingOrders_Carriers_CarrierId",
                table: "ShippingOrders",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingOrders_Shippers_ShipperId",
                table: "ShippingOrders",
                column: "ShipperId",
                principalTable: "Shippers",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingOrders_Carriers_CarrierId",
                table: "ShippingOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingOrders_Shippers_ShipperId",
                table: "ShippingOrders");

            migrationBuilder.DropTable(
                name: "shippingOrderLogs");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "ShippingOrders");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingOrders_DeliveryStatusId",
                table: "ShippingOrders",
                column: "DeliveryStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingOrders_Carriers_CarrierId",
                table: "ShippingOrders",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingOrders_DeliveryStatuses_DeliveryStatusId",
                table: "ShippingOrders",
                column: "DeliveryStatusId",
                principalTable: "DeliveryStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingOrders_Shippers_ShipperId",
                table: "ShippingOrders",
                column: "ShipperId",
                principalTable: "Shippers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
