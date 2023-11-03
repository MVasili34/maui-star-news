using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EntityModels.Migrations
{
    /// <inheritdoc />
    public partial class DefaultData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "role_id", "description", "name" },
                values: new object[,]
                {
                    { 1, "Обычный читатель с базовыми правами доступа", "Читатель" },
                    { 2, "Редактор с правами публикации статей", "Редактор" },
                    { 3, "Полный доступ ко всем возможностям и статистике приложения", "Администратор" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "role_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "role_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "role_id",
                keyValue: 3);
        }
    }
}
