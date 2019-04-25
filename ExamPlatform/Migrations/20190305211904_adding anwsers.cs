using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamPlatform.Migrations
{
    public partial class addinganwsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WrongAnswer_3",
                table: "ClosedQuestion",
                newName: "Answer_4");

            migrationBuilder.RenameColumn(
                name: "WrongAnswer_2",
                table: "ClosedQuestion",
                newName: "Answer_3");

            migrationBuilder.RenameColumn(
                name: "WrongAnswer_1",
                table: "ClosedQuestion",
                newName: "Answer_2");

            migrationBuilder.AddColumn<string>(
                name: "Answer_1",
                table: "ClosedQuestion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer_1",
                table: "ClosedQuestion");

            migrationBuilder.RenameColumn(
                name: "Answer_4",
                table: "ClosedQuestion",
                newName: "WrongAnswer_3");

            migrationBuilder.RenameColumn(
                name: "Answer_3",
                table: "ClosedQuestion",
                newName: "WrongAnswer_2");

            migrationBuilder.RenameColumn(
                name: "Answer_2",
                table: "ClosedQuestion",
                newName: "WrongAnswer_1");
        }
    }
}
