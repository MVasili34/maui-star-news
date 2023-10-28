using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityModels.Migrations
{
    /// <inheritdoc />
    public partial class ViewField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "views_count",
                table: "article",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "views_count",
                table: "article");
        }
    }
}
