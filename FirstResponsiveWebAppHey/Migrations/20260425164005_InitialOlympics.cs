using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FirstResponsiveWebAppHey.Migrations
{
    /// <inheritdoc />
    public partial class InitialOlympics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameID = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    GameID = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryID = table.Column<string>(type: "TEXT", nullable: false),
                    Sport = table.Column<string>(type: "TEXT", nullable: false),
                    LogoImage = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                    table.ForeignKey(
                        name: "FK_Countries_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Countries_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "Name" },
                values: new object[,]
                {
                    { "indoor", "Indoor" },
                    { "outdoor", "Outdoor" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameID", "Name" },
                values: new object[,]
                {
                    { "paralympics", "Paralympics" },
                    { "summer", "Summer Olympics" },
                    { "winter", "Winter Olympics" },
                    { "youth", "Youth Olympic Games" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "CategoryID", "GameID", "LogoImage", "Name", "Sport" },
                values: new object[,]
                {
                    { "aut", "outdoor", "paralympics", "aut.png", "Austria", "Canoe Sprint" },
                    { "bra", "outdoor", "summer", "bra.png", "Brazil", "Road Cycling" },
                    { "can", "indoor", "winter", "can.png", "Canada", "Curling" },
                    { "chn", "indoor", "summer", "chn.png", "China", "Diving" },
                    { "cyp", "indoor", "youth", "cyp.png", "Cyprus", "Breakdancing" },
                    { "fin", "outdoor", "youth", "fin.png", "Finland", "Skateboarding" },
                    { "fra", "indoor", "youth", "fra.png", "France", "Breakdancing" },
                    { "gbr", "indoor", "winter", "gbr.png", "Great Britain", "Curling" },
                    { "ger", "indoor", "summer", "ger.png", "Germany", "Diving" },
                    { "ita", "outdoor", "winter", "ita.png", "Italy", "Bobsleigh" },
                    { "jam", "outdoor", "winter", "jam.png", "Jamaica", "Bobsleigh" },
                    { "jpn", "outdoor", "winter", "jpn.png", "Japan", "Bobsleigh" },
                    { "mex", "indoor", "summer", "mex.png", "Mexico", "Diving" },
                    { "ned", "outdoor", "summer", "ned.png", "Netherlands", "Cycling" },
                    { "pak", "outdoor", "paralympics", "pak.png", "Pakistan", "Canoe Sprint" },
                    { "prt", "outdoor", "youth", "prt.png", "Portugal", "Skateboarding" },
                    { "rus", "indoor", "youth", "rus.png", "Russia", "Breakdancing" },
                    { "svk", "outdoor", "youth", "svk.png", "Slovakia", "Skateboarding" },
                    { "swe", "indoor", "winter", "swe.png", "Sweden", "Curling" },
                    { "tha", "indoor", "paralympics", "tha.png", "Thailand", "Archery" },
                    { "ukr", "indoor", "paralympics", "ukr.png", "Ukraine", "Archery" },
                    { "ury", "indoor", "paralympics", "ury.png", "Uruguay", "Archery" },
                    { "usa", "outdoor", "summer", "usa.png", "USA", "Road Cycling" },
                    { "zwe", "outdoor", "paralympics", "zwe.png", "Zimbabwe", "Canoe Sprint" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_CategoryID",
                table: "Countries",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_GameID",
                table: "Countries",
                column: "GameID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
