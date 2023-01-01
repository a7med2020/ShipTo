using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShipTo.Infrastructure.Migrations
{
    public partial class UpdateShippingOrderAddColBulk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shippingOrderLogs_Carriers_CarrierId",
                table: "shippingOrderLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_shippingOrderLogs_DeliveryStatuses_DeliveryStatusId",
                table: "shippingOrderLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_shippingOrderLogs_Shippers_ShipperId",
                table: "shippingOrderLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_shippingOrderLogs_ShippingOrders_ShippingOrderID",
                table: "shippingOrderLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_shippingOrderLogs_Users_CreatedBy",
                table: "shippingOrderLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_shippingOrderLogs_Users_ModefiedBy",
                table: "shippingOrderLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_shippingOrderLogs",
                table: "shippingOrderLogs");

            migrationBuilder.RenameTable(
                name: "shippingOrderLogs",
                newName: "ShippingOrderLogs");

            migrationBuilder.RenameIndex(
                name: "IX_shippingOrderLogs_ShippingOrderID",
                table: "ShippingOrderLogs",
                newName: "IX_ShippingOrderLogs_ShippingOrderID");

            migrationBuilder.RenameIndex(
                name: "IX_shippingOrderLogs_ShipperId",
                table: "ShippingOrderLogs",
                newName: "IX_ShippingOrderLogs_ShipperId");

            migrationBuilder.RenameIndex(
                name: "IX_shippingOrderLogs_ModefiedBy",
                table: "ShippingOrderLogs",
                newName: "IX_ShippingOrderLogs_ModefiedBy");

            migrationBuilder.RenameIndex(
                name: "IX_shippingOrderLogs_DeliveryStatusId",
                table: "ShippingOrderLogs",
                newName: "IX_ShippingOrderLogs_DeliveryStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_shippingOrderLogs_CreatedBy",
                table: "ShippingOrderLogs",
                newName: "IX_ShippingOrderLogs_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_shippingOrderLogs_CarrierId",
                table: "ShippingOrderLogs",
                newName: "IX_ShippingOrderLogs_CarrierId");

            migrationBuilder.AddColumn<string>(
                name: "BulkId",
                table: "ShippingOrders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingOrderBulkName",
                table: "ShippingOrders",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryDate",
                table: "ShippingOrderLogs",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingOrderLogs",
                table: "ShippingOrderLogs",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingOrderLogs_Carriers_CarrierId",
                table: "ShippingOrderLogs",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingOrderLogs_DeliveryStatuses_DeliveryStatusId",
                table: "ShippingOrderLogs",
                column: "DeliveryStatusId",
                principalTable: "DeliveryStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingOrderLogs_Shippers_ShipperId",
                table: "ShippingOrderLogs",
                column: "ShipperId",
                principalTable: "Shippers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingOrderLogs_ShippingOrders_ShippingOrderID",
                table: "ShippingOrderLogs",
                column: "ShippingOrderID",
                principalTable: "ShippingOrders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingOrderLogs_Users_CreatedBy",
                table: "ShippingOrderLogs",
                column: "CreatedBy",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingOrderLogs_Users_ModefiedBy",
                table: "ShippingOrderLogs",
                column: "ModefiedBy",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingOrderLogs_Carriers_CarrierId",
                table: "ShippingOrderLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingOrderLogs_DeliveryStatuses_DeliveryStatusId",
                table: "ShippingOrderLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingOrderLogs_Shippers_ShipperId",
                table: "ShippingOrderLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingOrderLogs_ShippingOrders_ShippingOrderID",
                table: "ShippingOrderLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingOrderLogs_Users_CreatedBy",
                table: "ShippingOrderLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingOrderLogs_Users_ModefiedBy",
                table: "ShippingOrderLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingOrderLogs",
                table: "ShippingOrderLogs");

            migrationBuilder.DropColumn(
                name: "BulkId",
                table: "ShippingOrders");

            migrationBuilder.DropColumn(
                name: "ShippingOrderBulkName",
                table: "ShippingOrders");

            migrationBuilder.RenameTable(
                name: "ShippingOrderLogs",
                newName: "shippingOrderLogs");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingOrderLogs_ShippingOrderID",
                table: "shippingOrderLogs",
                newName: "IX_shippingOrderLogs_ShippingOrderID");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingOrderLogs_ShipperId",
                table: "shippingOrderLogs",
                newName: "IX_shippingOrderLogs_ShipperId");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingOrderLogs_ModefiedBy",
                table: "shippingOrderLogs",
                newName: "IX_shippingOrderLogs_ModefiedBy");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingOrderLogs_DeliveryStatusId",
                table: "shippingOrderLogs",
                newName: "IX_shippingOrderLogs_DeliveryStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingOrderLogs_CreatedBy",
                table: "shippingOrderLogs",
                newName: "IX_shippingOrderLogs_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingOrderLogs_CarrierId",
                table: "shippingOrderLogs",
                newName: "IX_shippingOrderLogs_CarrierId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryDate",
                table: "shippingOrderLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_shippingOrderLogs",
                table: "shippingOrderLogs",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_shippingOrderLogs_Carriers_CarrierId",
                table: "shippingOrderLogs",
                column: "CarrierId",
                principalTable: "Carriers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_shippingOrderLogs_DeliveryStatuses_DeliveryStatusId",
                table: "shippingOrderLogs",
                column: "DeliveryStatusId",
                principalTable: "DeliveryStatuses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_shippingOrderLogs_Shippers_ShipperId",
                table: "shippingOrderLogs",
                column: "ShipperId",
                principalTable: "Shippers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_shippingOrderLogs_ShippingOrders_ShippingOrderID",
                table: "shippingOrderLogs",
                column: "ShippingOrderID",
                principalTable: "ShippingOrders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_shippingOrderLogs_Users_CreatedBy",
                table: "shippingOrderLogs",
                column: "CreatedBy",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_shippingOrderLogs_Users_ModefiedBy",
                table: "shippingOrderLogs",
                column: "ModefiedBy",
                principalSchema: "security",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
