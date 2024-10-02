using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnFlow.Migrations
{
  /// <inheritdoc />
  public partial class init : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Users",
          columns: table => new
          {
            UserId = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
            PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Role = table.Column<int>(type: "int", nullable: false),
            FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            DateJoined = table.Column<DateTime>(type: "datetime2", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Users", x => x.UserId);
          });

      migrationBuilder.CreateTable(
          name: "Courses",
          columns: table => new
          {
            CourseId = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            InstructorId = table.Column<int>(type: "int", nullable: false),
            Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
            CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Courses", x => x.CourseId);
            table.ForeignKey(
                      name: "FK_Courses_Users_InstructorId",
                      column: x => x.InstructorId,
                      principalTable: "Users",
                      principalColumn: "UserId",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Enrollments",
          columns: table => new
          {
            EnrollmentId = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            StudentId = table.Column<int>(type: "int", nullable: false),
            CourseId = table.Column<int>(type: "int", nullable: false),
            EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
            Progress = table.Column<float>(type: "real", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Enrollments", x => x.EnrollmentId);
            table.ForeignKey(
                      name: "FK_Enrollments_Courses_CourseId",
                      column: x => x.CourseId,
                      principalTable: "Courses",
                      principalColumn: "CourseId",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_Enrollments_Users_StudentId",
                      column: x => x.StudentId,
                      principalTable: "Users",
                      principalColumn: "UserId",
                      onDelete: ReferentialAction.NoAction);
          });

      migrationBuilder.CreateTable(
          name: "Lectures",
          columns: table => new
          {
            LectureId = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            CourseId = table.Column<int>(type: "int", nullable: false),
            Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Order = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Lectures", x => x.LectureId);
            table.ForeignKey(
                      name: "FK_Lectures_Courses_CourseId",
                      column: x => x.CourseId,
                      principalTable: "Courses",
                      principalColumn: "CourseId",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Payments",
          columns: table => new
          {
            PaymentId = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            StudentId = table.Column<int>(type: "int", nullable: false),
            CourseId = table.Column<int>(type: "int", nullable: false),
            Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
            PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Payments", x => x.PaymentId);
            table.ForeignKey(
                      name: "FK_Payments_Courses_CourseId",
                      column: x => x.CourseId,
                      principalTable: "Courses",
                      principalColumn: "CourseId",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_Payments_Users_StudentId",
                      column: x => x.StudentId,
                      principalTable: "Users",
                      principalColumn: "UserId",
                      onDelete: ReferentialAction.NoAction);
          });

      migrationBuilder.CreateTable(
          name: "Quizs",
          columns: table => new
          {
            QuizId = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            CourseId = table.Column<int>(type: "int", nullable: false),
            Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            MaxScore = table.Column<double>(type: "float", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Quizs", x => x.QuizId);
            table.ForeignKey(
                      name: "FK_Quizs_Courses_CourseId",
                      column: x => x.CourseId,
                      principalTable: "Courses",
                      principalColumn: "CourseId",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Reviews",
          columns: table => new
          {
            ReviewId = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            CourseId = table.Column<int>(type: "int", nullable: false),
            StudentId = table.Column<int>(type: "int", nullable: false),
            Rating = table.Column<int>(type: "int", nullable: false),
            ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Reviews", x => x.ReviewId);
            table.ForeignKey(
                      name: "FK_Reviews_Courses_CourseId",
                      column: x => x.CourseId,
                      principalTable: "Courses",
                      principalColumn: "CourseId",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_Reviews_Users_StudentId",
                      column: x => x.StudentId,
                      principalTable: "Users",
                      principalColumn: "UserId",
                      onDelete: ReferentialAction.NoAction);
          });

      migrationBuilder.CreateTable(
          name: "Questions",
          columns: table => new
          {
            QuestionId = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            QuizId = table.Column<int>(type: "int", nullable: false),
            QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Questions", x => x.QuestionId);
            table.ForeignKey(
                      name: "FK_Questions_Quizs_QuizId",
                      column: x => x.QuizId,
                      principalTable: "Quizs",
                      principalColumn: "QuizId",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "QuizResults",
          columns: table => new
          {
            ResultId = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            StudentId = table.Column<int>(type: "int", nullable: false),
            QuizId = table.Column<int>(type: "int", nullable: false),
            Score = table.Column<int>(type: "int", nullable: false),
            CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_QuizResults", x => x.ResultId);
            table.ForeignKey(
                      name: "FK_QuizResults_Quizs_QuizId",
                      column: x => x.QuizId,
                      principalTable: "Quizs",
                      principalColumn: "QuizId",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_QuizResults_Users_StudentId",
                      column: x => x.StudentId,
                      principalTable: "Users",
                      principalColumn: "UserId",
                      onDelete: ReferentialAction.NoAction);
          });

      migrationBuilder.CreateTable(
          name: "AnswerOptions",
          columns: table => new
          {
            AnswerOptionId = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            OptionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
            IsCorrect = table.Column<bool>(type: "bit", nullable: false),
            QuestionId = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_AnswerOptions", x => x.AnswerOptionId);
            table.ForeignKey(
                      name: "FK_AnswerOptions_Questions_QuestionId",
                      column: x => x.QuestionId,
                      principalTable: "Questions",
                      principalColumn: "QuestionId",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_AnswerOptions_QuestionId",
          table: "AnswerOptions",
          column: "QuestionId");

      migrationBuilder.CreateIndex(
          name: "IX_Courses_InstructorId",
          table: "Courses",
          column: "InstructorId");

      migrationBuilder.CreateIndex(
          name: "IX_Enrollments_CourseId",
          table: "Enrollments",
          column: "CourseId");

      migrationBuilder.CreateIndex(
          name: "IX_Enrollments_StudentId",
          table: "Enrollments",
          column: "StudentId");

      migrationBuilder.CreateIndex(
          name: "IX_Lectures_CourseId",
          table: "Lectures",
          column: "CourseId");

      migrationBuilder.CreateIndex(
          name: "IX_Payments_CourseId",
          table: "Payments",
          column: "CourseId");

      migrationBuilder.CreateIndex(
          name: "IX_Payments_StudentId",
          table: "Payments",
          column: "StudentId");

      migrationBuilder.CreateIndex(
          name: "IX_Questions_QuizId",
          table: "Questions",
          column: "QuizId");

      migrationBuilder.CreateIndex(
          name: "IX_QuizResults_QuizId",
          table: "QuizResults",
          column: "QuizId");

      migrationBuilder.CreateIndex(
          name: "IX_QuizResults_StudentId",
          table: "QuizResults",
          column: "StudentId");

      migrationBuilder.CreateIndex(
          name: "IX_Quizs_CourseId",
          table: "Quizs",
          column: "CourseId");

      migrationBuilder.CreateIndex(
          name: "IX_Reviews_CourseId",
          table: "Reviews",
          column: "CourseId");

      migrationBuilder.CreateIndex(
          name: "IX_Reviews_StudentId",
          table: "Reviews",
          column: "StudentId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "AnswerOptions");

      migrationBuilder.DropTable(
          name: "Enrollments");

      migrationBuilder.DropTable(
          name: "Lectures");

      migrationBuilder.DropTable(
          name: "Payments");

      migrationBuilder.DropTable(
          name: "QuizResults");

      migrationBuilder.DropTable(
          name: "Reviews");

      migrationBuilder.DropTable(
          name: "Questions");

      migrationBuilder.DropTable(
          name: "Quizs");

      migrationBuilder.DropTable(
          name: "Courses");

      migrationBuilder.DropTable(
          name: "Users");
    }
  }
}
