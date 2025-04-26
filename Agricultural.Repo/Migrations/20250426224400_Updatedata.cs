using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agricultural.Repo.Migrations
{
    /// <inheritdoc />
    public partial class Updatedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HumidityMax",
                table: "PlantInfos");

            migrationBuilder.DropColumn(
                name: "HumidityMin",
                table: "PlantInfos");

            migrationBuilder.DropColumn(
                name: "TemperatureMax",
                table: "PlantInfos");

            migrationBuilder.DropColumn(
                name: "TemperatureMin",
                table: "PlantInfos");

            migrationBuilder.RenameColumn(
                name: "WaterNeeds",
                table: "PlantInfos",
                newName: "Details_Water");

            migrationBuilder.RenameColumn(
                name: "Uses",
                table: "PlantInfos",
                newName: "Details_Temperature");

            migrationBuilder.RenameColumn(
                name: "SunlightNeeds",
                table: "PlantInfos",
                newName: "Details_Sunlight");

            migrationBuilder.RenameColumn(
                name: "SoilType",
                table: "PlantInfos",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "SeasonType",
                table: "PlantInfos",
                newName: "Edibility");

            migrationBuilder.RenameColumn(
                name: "PestControl",
                table: "PlantInfos",
                newName: "Details_Repotting");

            migrationBuilder.RenameColumn(
                name: "GrowthTime",
                table: "PlantInfos",
                newName: "Details_Pests");

            migrationBuilder.RenameColumn(
                name: "FertilizerType",
                table: "PlantInfos",
                newName: "Details_Fertilizing");

            migrationBuilder.RenameColumn(
                name: "CommonDiseases",
                table: "PlantInfos",
                newName: "About");

            migrationBuilder.AddColumn<string>(
                name: "CareLevel",
                table: "PlantInfos",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Flowering",
                table: "PlantInfos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAirPurifying",
                table: "PlantInfos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Medicinal",
                table: "PlantInfos",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ScientificName",
                table: "PlantInfos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CareLevel",
                table: "PlantInfos");

            migrationBuilder.DropColumn(
                name: "Flowering",
                table: "PlantInfos");

            migrationBuilder.DropColumn(
                name: "IsAirPurifying",
                table: "PlantInfos");

            migrationBuilder.DropColumn(
                name: "Medicinal",
                table: "PlantInfos");

            migrationBuilder.DropColumn(
                name: "ScientificName",
                table: "PlantInfos");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "PlantInfos",
                newName: "SoilType");

            migrationBuilder.RenameColumn(
                name: "Edibility",
                table: "PlantInfos",
                newName: "SeasonType");

            migrationBuilder.RenameColumn(
                name: "Details_Water",
                table: "PlantInfos",
                newName: "WaterNeeds");

            migrationBuilder.RenameColumn(
                name: "Details_Temperature",
                table: "PlantInfos",
                newName: "Uses");

            migrationBuilder.RenameColumn(
                name: "Details_Sunlight",
                table: "PlantInfos",
                newName: "SunlightNeeds");

            migrationBuilder.RenameColumn(
                name: "Details_Repotting",
                table: "PlantInfos",
                newName: "PestControl");

            migrationBuilder.RenameColumn(
                name: "Details_Pests",
                table: "PlantInfos",
                newName: "GrowthTime");

            migrationBuilder.RenameColumn(
                name: "Details_Fertilizing",
                table: "PlantInfos",
                newName: "FertilizerType");

            migrationBuilder.RenameColumn(
                name: "About",
                table: "PlantInfos",
                newName: "CommonDiseases");

            migrationBuilder.AddColumn<double>(
                name: "HumidityMax",
                table: "PlantInfos",
                type: "numeric(5,2)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "HumidityMin",
                table: "PlantInfos",
                type: "numeric(5,2)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TemperatureMax",
                table: "PlantInfos",
                type: "numeric(5,2)",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TemperatureMin",
                table: "PlantInfos",
                type: "numeric(5,2)",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
