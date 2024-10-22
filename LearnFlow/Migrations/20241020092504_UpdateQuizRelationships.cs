using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnFlow.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuizRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizResults_Quizs_QuizId",
                table: "QuizResults");

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
