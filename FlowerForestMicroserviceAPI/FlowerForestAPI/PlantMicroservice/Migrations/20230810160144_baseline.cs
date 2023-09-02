using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class baseline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catalogues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxHeight_metres = table.Column<double>(type: "float", nullable: false),
                    Genus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CatalogueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plants_Catalogues_CatalogueId",
                        column: x => x.CatalogueId,
                        principalTable: "Catalogues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plants_CatalogueId",
                table: "Plants",
                column: "CatalogueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "Catalogues");
        }
    }
}
