using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamPlatform.Migrations
{
    public partial class AddOpenedQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserAnswer",
                table: "OpenedQuestion",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExamOpenedQuestions",
                columns: table => new
                {
                    ExamOpenedQuestionsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExamsID = table.Column<int>(nullable: true),
                    OpenedQuestionsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamOpenedQuestions", x => x.ExamOpenedQuestionsID);
                    table.ForeignKey(
                        name: "FK_ExamOpenedQuestions_Exam_ExamsID",
                        column: x => x.ExamsID,
                        principalTable: "Exam",
                        principalColumn: "ExamsID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamOpenedQuestions_OpenedQuestion_OpenedQuestionsId",
                        column: x => x.OpenedQuestionsId,
                        principalTable: "OpenedQuestion",
                        principalColumn: "OpenedQuestionsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamOpenedQuestions_ExamsID",
                table: "ExamOpenedQuestions",
                column: "ExamsID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamOpenedQuestions_OpenedQuestionsId",
                table: "ExamOpenedQuestions",
                column: "OpenedQuestionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamOpenedQuestions");

            migrationBuilder.DropColumn(
                name: "UserAnswer",
                table: "OpenedQuestion");
        }
    }
}
