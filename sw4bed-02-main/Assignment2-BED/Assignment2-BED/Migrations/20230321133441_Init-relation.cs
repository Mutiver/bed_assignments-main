using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment2_BED.Migrations
{
    /// <inheritdoc />
    public partial class Initrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "job",
                columns: table => new
                {
                    JobId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_job", x => x.JobId);
                });

            migrationBuilder.CreateTable(
                name: "model",
                columns: table => new
                {
                    ModelId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    AddresLine1 = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    AddresLine2 = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Zip = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    City = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    BirthDay = table.Column<DateTime>(type: "date", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    ShoeSize = table.Column<int>(type: "int", nullable: false),
                    HairColor = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_model", x => x.ModelId);
                });

            migrationBuilder.CreateTable(
                name: "expense",
                columns: table => new
                {
                    ExpenseId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelId = table.Column<long>(type: "bigint", nullable: false),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<decimal>(type: "decimal(9,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expense", x => x.ExpenseId);
                    table.ForeignKey(
                        name: "FK_expense_job_JobId",
                        column: x => x.JobId,
                        principalTable: "job",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_expense_model_ModelId",
                        column: x => x.ModelId,
                        principalTable: "model",
                        principalColumn: "ModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobModel",
                columns: table => new
                {
                    JobsJobId = table.Column<long>(type: "bigint", nullable: false),
                    ModelsModelId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobModel", x => new { x.JobsJobId, x.ModelsModelId });
                    table.ForeignKey(
                        name: "FK_JobModel_job_JobsJobId",
                        column: x => x.JobsJobId,
                        principalTable: "job",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobModel_model_ModelsModelId",
                        column: x => x.ModelsModelId,
                        principalTable: "model",
                        principalColumn: "ModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_expense_JobId",
                table: "expense",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_expense_ModelId",
                table: "expense",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_JobModel_ModelsModelId",
                table: "JobModel",
                column: "ModelsModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expense");

            migrationBuilder.DropTable(
                name: "JobModel");

            migrationBuilder.DropTable(
                name: "job");

            migrationBuilder.DropTable(
                name: "model");
        }
    }
}
