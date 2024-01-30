using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedQuizScore2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Scores_QuizId",
                table: "Scores",
                column: "QuizId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Quizzes_QuizId",
                table: "Scores",
                column: "QuizId",
                principalTable: "Quizzes",
                principalColumn: "QuizId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Quizzes_QuizId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_QuizId",
                table: "Scores");
        }
    }
}
