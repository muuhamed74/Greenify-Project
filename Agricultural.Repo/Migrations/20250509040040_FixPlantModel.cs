using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agricultural.Repo.Migrations
{
    /// <inheritdoc />
    public partial class FixPlantModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HealthBenefits",
                table: "Plant");

            migrationBuilder.DropColumn(
                name: "Vitamins",
                table: "Plant");

            migrationBuilder.AddColumn<string>(
                name: "HealthBenefitsJson",
                table: "Plant",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VitaminsJson",
                table: "Plant",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HealthBenefitsJson",
                table: "Plant");

            migrationBuilder.DropColumn(
                name: "VitaminsJson",
                table: "Plant");

            migrationBuilder.AddColumn<List<string>>(
                name: "HealthBenefits",
                table: "Plant",
                type: "jsonb",
                nullable: false);

            migrationBuilder.AddColumn<List<string>>(
                name: "Vitamins",
                table: "Plant",
                type: "jsonb",
                nullable: false);
        }
    }
}
