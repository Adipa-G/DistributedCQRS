using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DistCqrs.Sample.Service.Product.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Product",
                table => new
                         {
                             Id = table.Column<Guid>(
                                 "uniqueidentifier",
                                 nullable: false),
                             Code = table.Column<string>(
                                 "nvarchar(max)",
                                 nullable: true),
                             IsDeleted =
                             table.Column<bool>("bit",
                                 nullable: false),
                             Name = table.Column<string>(
                                 "nvarchar(max)",
                                 nullable: true),
                             UnitPrice =
                             table.Column<double>("float",
                                 nullable: false)
                         },
                constraints: table =>
                             {
                                 table.PrimaryKey("PK_Product", x => x.Id);
                             });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Product");
        }
    }
}