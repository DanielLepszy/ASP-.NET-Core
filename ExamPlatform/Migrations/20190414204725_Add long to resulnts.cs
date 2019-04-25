using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamPlatform.Migrations
{
    public partial class Addlongtoresulnts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Score",
                table: "Results",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "AnswerPoints",
                table: "ExamOpenedQuestions",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "Results",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "AnswerPoints",
                table: "ExamOpenedQuestions",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
