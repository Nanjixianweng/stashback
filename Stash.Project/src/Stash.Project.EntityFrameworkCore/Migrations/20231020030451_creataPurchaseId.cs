using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stash.Project.Migrations
{
    /// <inheritdoc />
    public partial class creataPurchaseId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SellId",
                table: "SellProductRelationshipTable",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PurchaseId",
                table: "PurchaseProductRelationshipTable",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellId",
                table: "SellProductRelationshipTable");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "PurchaseProductRelationshipTable");
        }
    }
}
