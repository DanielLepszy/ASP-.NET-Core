﻿// <auto-generated />
using System;
using ExamPlatform.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExamPlatform.Migrations
{
    [DbContext(typeof(ExamPlatformDbContext))]
    [Migration("20190303210312_new1")]
    partial class new1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExamPlatformDataModel.Accounts", b =>
                {
                    b.Property<int>("AccountsID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("AccountsID");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("ExamPlatformDataModel.ClosedQuestions", b =>
                {
                    b.Property<int>("ClosedQuestionsID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnswerPoints");

                    b.Property<int>("CourseID");

                    b.Property<string>("ProperAnswer");

                    b.Property<string>("Question");

                    b.Property<string>("WrongAnswer_1");

                    b.Property<string>("WrongAnswer_2");

                    b.Property<string>("WrongAnswer_3");

                    b.HasKey("ClosedQuestionsID");

                    b.HasIndex("CourseID");

                    b.ToTable("ClosedQuestion");
                });

            modelBuilder.Entity("ExamPlatformDataModel.Course", b =>
                {
                    b.Property<int>("CourseID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CourseType");

                    b.HasKey("CourseID");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("ExamPlatformDataModel.ExamClosedQuestions", b =>
                {
                    b.Property<int>("ExamClosedQuestionsID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClosedQuestionsId");

                    b.Property<int?>("ExamsID");

                    b.HasKey("ExamClosedQuestionsID");

                    b.HasIndex("ClosedQuestionsId");

                    b.HasIndex("ExamsID");

                    b.ToTable("ExamClosedQuestions");
                });

            modelBuilder.Entity("ExamPlatformDataModel.Exams", b =>
                {
                    b.Property<int>("ExamsID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountID");

                    b.Property<int>("AmountClosedQuestions");

                    b.Property<int>("AmountOpenedQuestions");

                    b.Property<int>("CourseID");

                    b.Property<DateTime>("DateOfExam");

                    b.Property<int>("ExamTimeInMinute");

                    b.HasKey("ExamsID");

                    b.HasIndex("AccountID");

                    b.HasIndex("CourseID");

                    b.ToTable("Exam");
                });

            modelBuilder.Entity("ExamPlatformDataModel.OpenedQuestions", b =>
                {
                    b.Property<int>("OpenedQuestionsID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnswerPoints");

                    b.Property<int>("CourseID");

                    b.Property<int>("MaxPoints");

                    b.Property<string>("Question");

                    b.HasKey("OpenedQuestionsID");

                    b.HasIndex("CourseID");

                    b.ToTable("OpenedQuestion");
                });

            modelBuilder.Entity("ExamPlatformDataModel.ClosedQuestions", b =>
                {
                    b.HasOne("ExamPlatformDataModel.Course", "Course")
                        .WithMany("ClosedQuestionsList")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ExamPlatformDataModel.ExamClosedQuestions", b =>
                {
                    b.HasOne("ExamPlatformDataModel.ClosedQuestions", "ClosedQuestions")
                        .WithMany("ExamClosedQuestions")
                        .HasForeignKey("ClosedQuestionsId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ExamPlatformDataModel.Exams", "Exam")
                        .WithMany("ExamClosedQuestions")
                        .HasForeignKey("ExamsID");
                });

            modelBuilder.Entity("ExamPlatformDataModel.Exams", b =>
                {
                    b.HasOne("ExamPlatformDataModel.Accounts", "Account")
                        .WithMany("SelectedExam")
                        .HasForeignKey("AccountID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ExamPlatformDataModel.Course", "Course")
                        .WithMany("ExamsList")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ExamPlatformDataModel.OpenedQuestions", b =>
                {
                    b.HasOne("ExamPlatformDataModel.Course", "Course")
                        .WithMany("OpenedQuestionsList")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
