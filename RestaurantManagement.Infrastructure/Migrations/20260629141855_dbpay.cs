using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbpay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.AddColumn<int>(
                name: "MemberCardMemberId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TableId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);


            migrationBuilder.CreateTable(
                name: "DiningTables",
                columns: table => new
                {
                    TableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    CurrentStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiningTables", x => x.TableId);
                });

            migrationBuilder.CreateTable(
                name: "MemberCards",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoyaltyPoints = table.Column<int>(type: "int", nullable: false),
                    CardId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberCards", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: true),
                    MemberCardMemberId = table.Column<int>(type: "int", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_MemberCards_MemberCardMemberId",
                        column: x => x.MemberCardMemberId,
                        principalTable: "MemberCards",
                        principalColumn: "MemberId");
                    table.ForeignKey(
                        name: "FK_Invoices_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MemberCardMemberId",
                table: "Orders",
                column: "MemberCardMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TableId",
                table: "Orders",
                column: "TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_MemberCardMemberId",
                table: "Invoices",
                column: "MemberCardMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_OrderId",
                table: "Invoices",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DiningTables_TableId",
                table: "Orders",
                column: "TableId",
                principalTable: "DiningTables",
                principalColumn: "TableId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_MemberCards_MemberCardMemberId",
                table: "Orders",
                column: "MemberCardMemberId",
                principalTable: "MemberCards",
                principalColumn: "MemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DiningTables_TableId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_MemberCards_MemberCardMemberId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "DiningTables");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "MemberCards");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MemberCardMemberId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TableId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MemberCardMemberId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TableId",
                table: "Orders");

          
        }
    }
}
