using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Agricultural.Repo.Migrations
{
    /// <inheritdoc />
    public partial class Lastmodifi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ScientificName = table.Column<string>(type: "text", nullable: false),
                    GrowingConditions_Sunlight = table.Column<string>(type: "text", nullable: false),
                    GrowingConditions_Water = table.Column<string>(type: "text", nullable: false),
                    GrowingConditions_SoilType = table.Column<string>(type: "text", nullable: false),
                    Flower_Color = table.Column<string>(type: "text", nullable: false),
                    Flower_Morphology = table.Column<string>(type: "text", nullable: false),
                    Vitamins = table.Column<List<string>>(type: "jsonb", nullable: false),
                    HealthBenefits = table.Column<List<string>>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlantAdditionalData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlantName = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Treatment = table.Column<string>(type: "text", nullable: true),
                    AdditionalInformation = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantAdditionalData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlantInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlantName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ScientificName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CareLevel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Size = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Edibility = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Flowering = table.Column<bool>(type: "boolean", nullable: false),
                    Medicinal = table.Column<bool>(type: "boolean", nullable: false),
                    IsAirPurifying = table.Column<bool>(type: "boolean", nullable: false),
                    About = table.Column<string>(type: "text", nullable: false),
                    Details_Temperature = table.Column<string>(type: "text", nullable: false),
                    Details_Sunlight = table.Column<string>(type: "text", nullable: false),
                    Details_Water = table.Column<string>(type: "text", nullable: false),
                    Details_Repotting = table.Column<string>(type: "text", nullable: false),
                    Details_Fertilizing = table.Column<string>(type: "text", nullable: false),
                    Details_Pests = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlantImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ImageUrl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PlantsInfoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantImages_PlantInfos_PlantsInfoId",
                        column: x => x.PlantsInfoId,
                        principalTable: "PlantInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlantImages_PlantsInfoId",
                table: "PlantImages",
                column: "PlantsInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plant");

            migrationBuilder.DropTable(
                name: "PlantAdditionalData");

            migrationBuilder.DropTable(
                name: "PlantImages");

            migrationBuilder.DropTable(
                name: "PlantInfos");
        }
    }
}
