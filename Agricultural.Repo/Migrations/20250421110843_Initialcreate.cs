using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agricultural.Repo.Migrations
{
    /// <inheritdoc />
    public partial class Initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BotResponse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuestSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageAnalyses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    Analysis_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageAnalyses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlantInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SeasonType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SoilType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TemperatureMin = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TemperatureMax = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    HumidityMin = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    HumidityMax = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    WaterNeeds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SunlightNeeds = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrowthTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FertilizerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommonDiseases = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PestControl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uses = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadedImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadedImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlantImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PlantsInfoId = table.Column<int>(type: "int", nullable: false)
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
                name: "ChatHistories");

            migrationBuilder.DropTable(
                name: "GuestSessions");

            migrationBuilder.DropTable(
                name: "ImageAnalyses");

            migrationBuilder.DropTable(
                name: "PlantImages");

            migrationBuilder.DropTable(
                name: "UploadedImages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PlantInfos");
        }
    }
}
