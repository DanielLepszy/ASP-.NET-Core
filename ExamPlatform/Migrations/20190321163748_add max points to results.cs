using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamPlatform.Migrations
{
    public partial class addmaxpointstoresults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Grade",
                table: "Results",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "MaxExamPoints",
                table: "Results",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "AnswerPoints",
                table: "ExamOpenedQuestions",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxExamPoints",
                table: "Results");

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "Results",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "AnswerPoints",
                table: "ExamOpenedQuestions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
