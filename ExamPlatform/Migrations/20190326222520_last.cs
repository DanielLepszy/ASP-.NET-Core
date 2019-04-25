using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamPlatform.Migrations
{
    public partial class last : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Grade",
                table: "Results",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Grade",
                table: "Results",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
