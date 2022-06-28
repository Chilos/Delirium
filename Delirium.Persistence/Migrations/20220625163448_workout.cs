using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delirium.Persistence.Migrations
{
    public partial class workout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseTemplateWorkout",
                columns: table => new
                {
                    ExercisesId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkoutsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTemplateWorkout", x => new { x.ExercisesId, x.WorkoutsId });
                    table.ForeignKey(
                        name: "FK_ExerciseTemplateWorkout_ExerciseTemplates_ExercisesId",
                        column: x => x.ExercisesId,
                        principalTable: "ExerciseTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseTemplateWorkout_Workouts_WorkoutsId",
                        column: x => x.WorkoutsId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sets_ExerciseTemplates_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "ExerciseTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sets_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MeasurementId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<double>(type: "double precision", nullable: false),
                    SetId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeasurementValues_Measurements_MeasurementId",
                        column: x => x.MeasurementId,
                        principalTable: "Measurements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasurementValues_Sets_SetId",
                        column: x => x.SetId,
                        principalTable: "Sets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTemplateWorkout_WorkoutsId",
                table: "ExerciseTemplateWorkout",
                column: "WorkoutsId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementValues_Id",
                table: "MeasurementValues",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementValues_MeasurementId",
                table: "MeasurementValues",
                column: "MeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_MeasurementValues_SetId",
                table: "MeasurementValues",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_Sets_ExerciseId",
                table: "Sets",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Sets_Id",
                table: "Sets",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sets_WorkoutId",
                table: "Sets",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_Id",
                table: "Workouts",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseTemplateWorkout");

            migrationBuilder.DropTable(
                name: "MeasurementValues");

            migrationBuilder.DropTable(
                name: "Sets");

            migrationBuilder.DropTable(
                name: "Workouts");
        }
    }
}
