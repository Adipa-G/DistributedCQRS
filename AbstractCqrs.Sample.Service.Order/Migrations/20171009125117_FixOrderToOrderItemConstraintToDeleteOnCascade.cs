using Microsoft.EntityFrameworkCore.Migrations;

namespace AbstractCqrs.Sample.Service.Order.Migrations
{
    public partial class FixOrderToOrderItemConstraintToDeleteOnCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orderitem_Order_OrderId",
                table: "Orderitem");

            migrationBuilder.AddForeignKey(
                name: "FK_Orderitem_Order_OrderId",
                table: "Orderitem",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orderitem_Order_OrderId",
                table: "Orderitem");

            migrationBuilder.AddForeignKey(
                name: "FK_Orderitem_Order_OrderId",
                table: "Orderitem",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
