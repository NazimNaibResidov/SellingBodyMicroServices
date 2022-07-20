using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogService.Api.Migrations
{
    public partial class itinatl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "catalog");

            migrationBuilder.CreateSequence(
                name: "Catalog_Brend_Id",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Catalog_Item_Id",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "Catalog_Type_Id",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "CatalogBrend",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Brend = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogBrend", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogType",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItem",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CatalogTypeId = table.Column<int>(type: "int", nullable: false),
                    CatalogBrendId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogItem_CatalogBrend_CatalogBrendId",
                        column: x => x.CatalogBrendId,
                        principalSchema: "catalog",
                        principalTable: "CatalogBrend",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogItem_CatalogType_CatalogTypeId",
                        column: x => x.CatalogTypeId,
                        principalSchema: "catalog",
                        principalTable: "CatalogType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItem_CatalogBrendId",
                schema: "catalog",
                table: "CatalogItem",
                column: "CatalogBrendId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItem_CatalogTypeId",
                schema: "catalog",
                table: "CatalogItem",
                column: "CatalogTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogItem",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "CatalogBrend",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "CatalogType",
                schema: "catalog");

            migrationBuilder.DropSequence(
                name: "Catalog_Brend_Id");

            migrationBuilder.DropSequence(
                name: "Catalog_Item_Id");

            migrationBuilder.DropSequence(
                name: "Catalog_Type_Id");
        }
    }
}
