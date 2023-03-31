using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book.Repository.Migrations
{
    /// <inheritdoc />
    public partial class updatelist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "ListItems");

            migrationBuilder.DropColumn(
                name: "GoalScore",
                table: "ListItems");

            migrationBuilder.AlterColumn<string>(
                name: "Risk",
                table: "ListItems",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContentId",
                table: "ListItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ListItems_ContentId",
                table: "ListItems",
                column: "ContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ListItems_Contents_ContentId",
                table: "ListItems",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListItems_Contents_ContentId",
                table: "ListItems");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_ListItems_ContentId",
                table: "ListItems");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "ListItems");

            migrationBuilder.AlterColumn<string>(
                name: "Risk",
                table: "ListItems",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "ListItems",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GoalScore",
                table: "ListItems",
                type: "integer",
                nullable: true);
        }
    }
}
