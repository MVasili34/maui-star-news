using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EntityModels.Migrations
{
    /// <inheritdoc />
    public partial class ExtraDefaultData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "section",
                columns: new[] { "section_id", "description", "material_icon", "name" },
                values: new object[,]
                {
                    { 1, "Новости о политике", "", "Политика" },
                    { 2, "Сводки о прошлом, настоящем и будущем", "", "История" },
                    { 3, "Новости о нововведениях в системе образования", "", "Образование" },
                    { 4, "Новости о предпринимательстве", "", "Бизнес" },
                    { 5, "Новости об управлении", "", "Маркетинг" },
                    { 6, "Новости о природе", "", "Природа" },
                    { 7, "Нововведения в медицине", "", "Здоровье" },
                    { 8, "Новости о соревнованиях", "", "Спорт" },
                    { 9, "Новые премьеры", "", "Кино" },
                    { 10, "Новые прорыва", "", "Наука" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "section",
                keyColumn: "section_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "section",
                keyColumn: "section_id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "section",
                keyColumn: "section_id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "section",
                keyColumn: "section_id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "section",
                keyColumn: "section_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "section",
                keyColumn: "section_id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "section",
                keyColumn: "section_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "section",
                keyColumn: "section_id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "section",
                keyColumn: "section_id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "section",
                keyColumn: "section_id",
                keyValue: 10);
        }
    }
}
