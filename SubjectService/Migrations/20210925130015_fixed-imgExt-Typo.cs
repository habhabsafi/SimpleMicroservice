using Microsoft.EntityFrameworkCore.Migrations;

namespace SubjectService.Migrations
{
    public partial class fixedimgExtTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageExtenstion",
                table: "Subjects",
                newName: "ImageExtension");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageExtension",
                table: "Subjects",
                newName: "ImageExtenstion");
        }
    }
}
