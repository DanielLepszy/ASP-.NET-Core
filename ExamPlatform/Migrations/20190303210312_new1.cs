using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamPlatform.Migrations
{
    public partial class new1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Surname = table.Column<string>(maxLength: 20, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountsID);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseID);
                });

            migrationBuilder.CreateTable(
                name: "ClosedQuestion",
                columns: table => new
                {
                    ClosedQuestionsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Question = table.Column<string>(nullable: true),
                    ProperAnswer = table.Column<string>(nullable: true),
                    AnswerPoints = table.Column<int>(nullable: false),
                    WrongAnswer_1 = table.Column<string>(nullable: true),
                    WrongAnswer_2 = table.Column<string>(nullable: true),
                    WrongAnswer_3 = table.Column<string>(nullable: true),
                    CourseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClosedQuestion", x => x.ClosedQuestionsID);
                    table.ForeignKey(
                        name: "FK_ClosedQuestion_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    ExamsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmountClosedQuestions = table.Column<int>(nullable: false),
                    AmountOpenedQuestions = table.Column<int>(nullable: false),
                    ExamTimeInMinute = table.Column<int>(nullable: false),
                    DateOfExam = table.Column<DateTime>(nullable: false),
                    AccountID = table.Column<int>(nullable: false),
                    CourseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.ExamsID);
                    table.ForeignKey(
                        name: "FK_Exam_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "AccountsID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Exam_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OpenedQuestion",
                columns: table => new
                {
                    OpenedQuestionsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Question = table.Column<string>(nullable: true),
                    MaxPoints = table.Column<int>(nullable: false),
                    AnswerPoints = table.Column<int>(nullable: false),
                    CourseID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenedQuestion", x => x.OpenedQuestionsID);
                    table.ForeignKey(
                        name: "FK_OpenedQuestion_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExamClosedQuestions",
                columns: table => new
                {
                    ExamClosedQuestionsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExamsID = table.Column<int>(nullable: true),
                    ClosedQuestionsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamClosedQuestions", x => x.ExamClosedQuestionsID);
                    table.ForeignKey(
                        name: "FK_ExamClosedQuestions_ClosedQuestion_ClosedQuestionsId",
                        column: x => x.ClosedQuestionsId,
                        principalTable: "ClosedQuestion",
                        principalColumn: "ClosedQuestionsID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamClosedQuestions_Exam_ExamsID",
                        column: x => x.ExamsID,
                        principalTable: "Exam",
                        principalColumn: "ExamsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClosedQuestion_CourseID",
                table: "ClosedQuestion",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_AccountID",
                table: "Exam",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_CourseID",
                table: "Exam",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamClosedQuestions_ClosedQuestionsId",
                table: "ExamClosedQuestions",
                column: "ClosedQuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamClosedQuestions_ExamsID",
                table: "ExamClosedQuestions",
                column: "ExamsID");

            migrationBuilder.CreateIndex(
                name: "IX_OpenedQuestion_CourseID",
                table: "OpenedQuestion",
                column: "CourseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamClosedQuestions");

            migrationBuilder.DropTable(
                name: "OpenedQuestion");

            migrationBuilder.DropTable(
                name: "ClosedQuestion");

            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Course");
        }
    }
}
