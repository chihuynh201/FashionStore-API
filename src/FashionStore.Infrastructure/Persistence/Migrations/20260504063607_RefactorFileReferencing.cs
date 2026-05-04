using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionStore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactorFileReferencing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "SkuImages");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "SkuImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SkuImages_FileId",
                table: "SkuImages",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FileId",
                table: "Products",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_FileUploads_FileId",
                table: "Products",
                column: "FileId",
                principalTable: "FileUploads",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SkuImages_FileUploads_FileId",
                table: "SkuImages",
                column: "FileId",
                principalTable: "FileUploads",
                principalColumn: "FileId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_FileUploads_FileId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_SkuImages_FileUploads_FileId",
                table: "SkuImages");

            migrationBuilder.DropIndex(
                name: "IX_SkuImages_FileId",
                table: "SkuImages");

            migrationBuilder.DropIndex(
                name: "IX_Products_FileId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "SkuImages");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "SkuImages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Products",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }
    }
}
