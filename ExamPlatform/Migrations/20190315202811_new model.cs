using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamPlatform.Migrations
{
    public partial class newmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamOpenedQuestions_OpenedQuestion_OpenedQuestionsId",
                table: "ExamOpenedQuestions");

            migrationBuilder.DropColumn(
                name: "AnswerPoints",
                table: "OpenedQuestion");

            migrationBuilder.DropColumn(
                name: "UserAnswer",
                table: "OpenedQuestion");

            migrationBuilder.RenameColumn(
                name: "OpenedQuestionsId",
                table: "ExamOpenedQuestions",
                newName: "OpenedQuestionsID");

            migrationBuilder.RenameIndex(
                name: "IX_ExamOpenedQuestions_OpenedQuestionsId",
                table: "ExamOpenedQuestions",
                newName: "IX_ExamOpenedQuestions_OpenedQuestionsID");

            migrationBuilder.AddColumn<int>(
                name: "AnswerPoints",
                table: "ExamOpenedQuestions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserAnswer",
                table: "ExamOpenedQuestions",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamOpenedQuestions_OpenedQuestion_OpenedQuestionsID",
                table: "ExamOpenedQuestions",
                column: "OpenedQuestionsID",
                principalTable: "OpenedQuestion",
                principalColumn: "OpenedQuestionsID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamOpenedQuestions_OpenedQuestion_OpenedQuestionsID",
                table: "ExamOpenedQuestions");

            migrationBuilder.DropColumn(
                name: "AnswerPoints",
                table: "ExamOpenedQuestions");

            migrationBuilder.DropColumn(
                name: "UserAnswer",
                table: "ExamOpenedQuestions");

            migrationBuilder.RenameColumn(
                name: "OpenedQuestionsID",
                table: "ExamOpenedQuestions",
                newName: "OpenedQuestionsId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamOpenedQuestions_OpenedQuestionsID",
                table: "ExamOpenedQuestions",
                newName: "IX_ExamOpenedQuestions_OpenedQuestionsId");

            migrationBuilder.AddColumn<int>(
                name: "AnswerPoints",
                table: "OpenedQuestion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserAnswer",
                table: "OpenedQuestion",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamOpenedQuestions_OpenedQuestion_OpenedQuestionsId",
                table: "ExamOpenedQuestions",
                column: "OpenedQuestionsId",
                principalTable: "OpenedQuestion",
                principalColumn: "OpenedQuestionsID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
