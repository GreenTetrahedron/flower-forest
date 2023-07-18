using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlowerForestAPI.Migrations
{
    public partial class CatalogueRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CataloguedPlants");

            migrationBuilder.AddColumn<Guid>(
                name: "CatalogueId",
                table: "Plants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Catalogues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Catalogues_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plants_CatalogueId",
                table: "Plants",
                column: "CatalogueId");

            migrationBuilder.CreateIndex(
                name: "IX_Catalogues_UserId",
                table: "Catalogues",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plants_Catalogues_CatalogueId",
                table: "Plants",
                column: "CatalogueId",
                principalTable: "Catalogues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plants_Catalogues_CatalogueId",
                table: "Plants");

            migrationBuilder.DropTable(
                name: "Catalogues");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Plants_CatalogueId",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "CatalogueId",
                table: "Plants");

            migrationBuilder.CreateTable(
                name: "CataloguedPlants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxHeight_metres = table.Column<double>(type: "float", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CataloguedPlants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CataloguedPlants_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CataloguedPlants_UserId",
                table: "CataloguedPlants",
                column: "UserId");
        }
    }
}
