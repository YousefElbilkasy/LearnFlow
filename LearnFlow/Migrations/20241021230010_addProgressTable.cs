using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnFlow.Migrations
{
    /// <inheritdoc />
    public partial class addProgressTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizResults_Quizs_QuizId",
                table: "QuizResults");

            migrationBuilder.CreateTable(
                name: "Progresses",
                columns: table => new
                {
                    EnrollmentId = table.Column<int>(type: "int", nullable: false),
                    LectureId = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progresses", x => new { x.EnrollmentId, x.LectureId });
                    table.ForeignKey(
                        name: "FK_Progresses_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "EnrollmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Progresses_Lectures_LectureId",
                        column: x => x.LectureId,
                        principalTable: "Lectures",
                        principalColumn: "LectureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Progresses_LectureId",
                table: "Progresses",
                column: "LectureId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizResults_Quizs_QuizId",
                table: "QuizResults",
                column: "QuizId",
                principalTable: "Quizs",
                principalColumn: "QuizId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizResults_Quizs_QuizId",
                table: "QuizResults");

            migrationBuilder.DropTable(
                name: "Progresses");

            migrationBuilder.AddForeignKey(
                name: "FK_QuizResults_Quizs_QuizId",
                table: "QuizResults",
                column: "QuizId",
                principalTable: "Quizs",
                principalColumn: "QuizId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
