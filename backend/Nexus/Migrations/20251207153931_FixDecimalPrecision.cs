using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.API.Migrations
{
    /// <inheritdoc />
    public partial class FixDecimalPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PurchaseOrders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 12, 7, 15, 39, 30, 546, DateTimeKind.Utc).AddTicks(4338));

            migrationBuilder.UpdateData(
                table: "PurchaseOrders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 12, 7, 15, 39, 30, 546, DateTimeKind.Utc).AddTicks(4343));

            migrationBuilder.UpdateData(
                table: "SalesOrders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 12, 7, 15, 39, 30, 546, DateTimeKind.Utc).AddTicks(4397));

            migrationBuilder.UpdateData(
                table: "SalesOrders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 12, 7, 15, 39, 30, 546, DateTimeKind.Utc).AddTicks(4401));

            migrationBuilder.UpdateData(
                table: "StockTransactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "TransactionDate",
                value: new DateTime(2025, 12, 7, 15, 39, 30, 546, DateTimeKind.Utc).AddTicks(4446));

            migrationBuilder.UpdateData(
                table: "StockTransactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "TransactionDate",
                value: new DateTime(2025, 12, 7, 15, 39, 30, 546, DateTimeKind.Utc).AddTicks(4449));

            migrationBuilder.UpdateData(
                table: "StockTransactions",
                keyColumn: "Id",
                keyValue: 3,
                column: "TransactionDate",
                value: new DateTime(2025, 12, 7, 15, 39, 30, 546, DateTimeKind.Utc).AddTicks(4451));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PurchaseOrders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 12, 7, 15, 36, 22, 30, DateTimeKind.Utc).AddTicks(3354));

            migrationBuilder.UpdateData(
                table: "PurchaseOrders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 12, 7, 15, 36, 22, 30, DateTimeKind.Utc).AddTicks(3358));

            migrationBuilder.UpdateData(
                table: "SalesOrders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 12, 7, 15, 36, 22, 30, DateTimeKind.Utc).AddTicks(3403));

            migrationBuilder.UpdateData(
                table: "SalesOrders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 12, 7, 15, 36, 22, 30, DateTimeKind.Utc).AddTicks(3407));

            migrationBuilder.UpdateData(
                table: "StockTransactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "TransactionDate",
                value: new DateTime(2025, 12, 7, 15, 36, 22, 30, DateTimeKind.Utc).AddTicks(3437));

            migrationBuilder.UpdateData(
                table: "StockTransactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "TransactionDate",
                value: new DateTime(2025, 12, 7, 15, 36, 22, 30, DateTimeKind.Utc).AddTicks(3440));

            migrationBuilder.UpdateData(
                table: "StockTransactions",
                keyColumn: "Id",
                keyValue: 3,
                column: "TransactionDate",
                value: new DateTime(2025, 12, 7, 15, 36, 22, 30, DateTimeKind.Utc).AddTicks(3442));
        }
    }
}
