using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Equipment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryMuscleGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondaryMuscleGroup = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromptHistorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PromptText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponseText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromptHistorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PromptHistorys_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Goal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutPlans_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfTheWeek = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkoutPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutDays_WorkoutPlans_WorkoutPlanId",
                        column: x => x.WorkoutPlanId,
                        principalTable: "WorkoutPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sets = table.Column<int>(type: "int", nullable: false),
                    Reps = table.Column<int>(type: "int", nullable: false),
                    TargetWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TargetTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    WorkoutDayId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutExercises_WorkoutDays_WorkoutDayId",
                        column: x => x.WorkoutDayId,
                        principalTable: "WorkoutDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedSets = table.Column<int>(type: "int", nullable: false),
                    CompletedReps = table.Column<int>(type: "int", nullable: false),
                    ActualWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActualTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WorkoutExerciseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutLogs_WorkoutExercises_WorkoutExerciseId",
                        column: x => x.WorkoutExerciseId,
                        principalTable: "WorkoutExercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PromptHistorys_UserId",
                table: "PromptHistorys",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutDays_WorkoutPlanId",
                table: "WorkoutDays",
                column: "WorkoutPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_ExerciseId",
                table: "WorkoutExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_WorkoutDayId",
                table: "WorkoutExercises",
                column: "WorkoutDayId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutLogs_UserId",
                table: "WorkoutLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutLogs_WorkoutExerciseId",
                table: "WorkoutLogs",
                column: "WorkoutExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlans_UserId",
                table: "WorkoutPlans",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PromptHistorys");

            migrationBuilder.DropTable(
                name: "WorkoutLogs");

            migrationBuilder.DropTable(
                name: "WorkoutExercises");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "WorkoutDays");

            migrationBuilder.DropTable(
                name: "WorkoutPlans");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
