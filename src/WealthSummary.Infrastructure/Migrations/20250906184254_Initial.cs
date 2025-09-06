using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WealthSummary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "main");

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "main",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                schema: "main",
                columns: table => new
                {
                    AssetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    AssetType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.AssetId);
                    table.ForeignKey(
                        name: "FK_Assets_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "main",
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialStatuses",
                schema: "main",
                columns: table => new
                {
                    FinancialStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    AnnualIncome = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AnnualExpenses = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RiskAppetite = table.Column<int>(type: "int", nullable: false),
                    Goals = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialStatuses", x => x.FinancialStatusId);
                    table.ForeignKey(
                        name: "FK_FinancialStatuses_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "main",
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Liabilities",
                schema: "main",
                columns: table => new
                {
                    LiabilityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    LiabilityType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Liabilities", x => x.LiabilityId);
                    table.ForeignKey(
                        name: "FK_Liabilities_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "main",
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeetingNotes",
                schema: "main",
                columns: table => new
                {
                    MeetingNoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    MeetingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingNotes", x => x.MeetingNoteId);
                    table.ForeignKey(
                        name: "FK_MeetingNotes_Clients_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "main",
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ClientId",
                schema: "main",
                table: "Assets",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialStatuses_ClientId",
                schema: "main",
                table: "FinancialStatuses",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Liabilities_ClientId",
                schema: "main",
                table: "Liabilities",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingNotes_ClientId",
                schema: "main",
                table: "MeetingNotes",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets",
                schema: "main");

            migrationBuilder.DropTable(
                name: "FinancialStatuses",
                schema: "main");

            migrationBuilder.DropTable(
                name: "Liabilities",
                schema: "main");

            migrationBuilder.DropTable(
                name: "MeetingNotes",
                schema: "main");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "main");
        }
    }
}
