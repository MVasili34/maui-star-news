using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityModels.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_article_user_publisher_id",
                table: "article");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_role_id",
                table: "user");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "user",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "last_login",
                table: "user",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<Guid>(
                name: "publisher_id",
                table: "article",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_article_user_publisher_id",
                table: "article",
                column: "publisher_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_role_id",
                table: "user",
                column: "role_id",
                principalTable: "role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_article_user_publisher_id",
                table: "article");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_role_id",
                table: "user");

            migrationBuilder.DropColumn(
                name: "last_login",
                table: "user");

            migrationBuilder.AlterColumn<int>(
                name: "role_id",
                table: "user",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "publisher_id",
                table: "article",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_article_user_publisher_id",
                table: "article",
                column: "publisher_id",
                principalTable: "user",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_role_id",
                table: "user",
                column: "role_id",
                principalTable: "role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
