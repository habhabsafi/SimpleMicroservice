using Microsoft.EntityFrameworkCore.Migrations;

namespace SubjectService.Migrations
{
    public partial class addingimageExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageExtenstion",
                table: "Subjects",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageExtenstion",
                table: "Subjects");
        }
    }
}
