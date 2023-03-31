using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ondelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListItems_Categories_CategoryId",
                table: "ListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ListItems_Consepts_ConseptId",
                table: "ListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ListItems_ControlLists_ControlListId",
                table: "ListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ListItems_Segments_SegmentId",
                table: "ListItems");

            migrationBuilder.AddForeignKey(
                name: "FK_ListItems_Categories_CategoryId",
                table: "ListItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ListItems_Consepts_ConseptId",
                table: "ListItems",
                column: "ConseptId",
                principalTable: "Consepts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ListItems_ControlLists_ControlListId",
                table: "ListItems",
                column: "ControlListId",
                principalTable: "ControlLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ListItems_Segments_SegmentId",
                table: "ListItems",
                column: "SegmentId",
                principalTable: "Segments",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListItems_Categories_CategoryId",
                table: "ListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ListItems_Consepts_ConseptId",
                table: "ListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ListItems_ControlLists_ControlListId",
                table: "ListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ListItems_Segments_SegmentId",
                table: "ListItems");

            migrationBuilder.AddForeignKey(
                name: "FK_ListItems_Categories_CategoryId",
                table: "ListItems",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ListItems_Consepts_ConseptId",
                table: "ListItems",
                column: "ConseptId",
                principalTable: "Consepts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ListItems_ControlLists_ControlListId",
                table: "ListItems",
                column: "ControlListId",
                principalTable: "ControlLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ListItems_Segments_SegmentId",
                table: "ListItems",
                column: "SegmentId",
                principalTable: "Segments",
                principalColumn: "Id");
        }
    }
}
