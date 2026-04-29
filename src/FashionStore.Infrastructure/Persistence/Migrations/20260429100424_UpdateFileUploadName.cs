using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionStore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFileUploadName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FileUpload",
                table: "FileUpload");

            migrationBuilder.RenameTable(
                name: "FileUpload",
                newName: "FileUploads");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileUploads",
                table: "FileUploads",
                column: "FileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FileUploads",
                table: "FileUploads");

            migrationBuilder.RenameTable(
                name: "FileUploads",
                newName: "FileUpload");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileUpload",
                table: "FileUpload",
                column: "FileId");
        }
    }
}
