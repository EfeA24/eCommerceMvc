using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerceMvc.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "shopifyApiInfos",
                columns: table => new
                {
                    ShopDomain = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CanReadProducts = table.Column<bool>(type: "bit", nullable: false),
                    CanCreateProducts = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shopifyApiInfos", x => x.ShopDomain);
                });

            migrationBuilder.CreateTable(
                name: "shopifyProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopifyProductId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BodyHtml = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vendor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Handle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastSyncedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shopifyProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "symbols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_symbols", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trendyolApiInfos",
                columns: table => new
                {
                    SellerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecretKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Environment = table.Column<int>(type: "int", nullable: false),
                    IntegratorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductionBaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StageBaseUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StageIpAuthorized = table.Column<bool>(type: "bit", nullable: false),
                    StageAuthorizedIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanReadProducts = table.Column<bool>(type: "bit", nullable: false),
                    CanCreateProducts = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trendyolApiInfos", x => x.SellerId);
                });

            migrationBuilder.CreateTable(
                name: "trendyolBrands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrendyolBrandId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trendyolBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "trendyolCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrendyolCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trendyolCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "contentBases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageContentId = table.Column<int>(type: "int", nullable: false),
                    ImagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contentBases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contentBases_images_ImagesId",
                        column: x => x.ImagesId,
                        principalTable: "images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contentBases_pages_PageContentId",
                        column: x => x.PageContentId,
                        principalTable: "pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shopifyProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopifyImageId = table.Column<long>(type: "bigint", nullable: false),
                    Src = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Alt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShopifyProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shopifyProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shopifyProductImages_shopifyProducts_ShopifyProductId",
                        column: x => x.ShopifyProductId,
                        principalTable: "shopifyProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shopifyProductOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopifyOptionId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    ValuesCsv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShopifyProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shopifyProductOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shopifyProductOptions_shopifyProducts_ShopifyProductId",
                        column: x => x.ShopifyProductId,
                        principalTable: "shopifyProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shopifyVariants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShopifyVariantId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sku = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompareAtPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InventoryQuantity = table.Column<int>(type: "int", nullable: false),
                    Taxable = table.Column<bool>(type: "bit", nullable: false),
                    InventoryPolicy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InventoryManagement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeightUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShopifyProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shopifyVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_shopifyVariants_shopifyProducts_ShopifyProductId",
                        column: x => x.ShopifyProductId,
                        principalTable: "shopifyProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trendyolProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductMainId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ListPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    VatRate = table.Column<int>(type: "int", nullable: false),
                    CurrencyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    Archived = table.Column<bool>(type: "bit", nullable: false),
                    LastSyncedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrendyolCategoryId = table.Column<int>(type: "int", nullable: false),
                    TrendyolBrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trendyolProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trendyolProducts_trendyolBrands_TrendyolBrandId",
                        column: x => x.TrendyolBrandId,
                        principalTable: "trendyolBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trendyolProducts_trendyolCategories_TrendyolCategoryId",
                        column: x => x.TrendyolCategoryId,
                        principalTable: "trendyolCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trendyolProductAttributes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeId = table.Column<long>(type: "bigint", nullable: false),
                    AttributeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttributeValueId = table.Column<long>(type: "bigint", nullable: false),
                    AttributeValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrendyolProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trendyolProductAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trendyolProductAttributes_trendyolProducts_TrendyolProductId",
                        column: x => x.TrendyolProductId,
                        principalTable: "trendyolProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trendyolProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    TrendyolProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trendyolProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trendyolProductImages_trendyolProducts_TrendyolProductId",
                        column: x => x.TrendyolProductId,
                        principalTable: "trendyolProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_contentBases_ImagesId",
                table: "contentBases",
                column: "ImagesId");

            migrationBuilder.CreateIndex(
                name: "IX_contentBases_PageContentId",
                table: "contentBases",
                column: "PageContentId");

            migrationBuilder.CreateIndex(
                name: "IX_shopifyProductImages_ShopifyProductId",
                table: "shopifyProductImages",
                column: "ShopifyProductId");

            migrationBuilder.CreateIndex(
                name: "IX_shopifyProductOptions_ShopifyProductId",
                table: "shopifyProductOptions",
                column: "ShopifyProductId");

            migrationBuilder.CreateIndex(
                name: "IX_shopifyVariants_ShopifyProductId",
                table: "shopifyVariants",
                column: "ShopifyProductId");

            migrationBuilder.CreateIndex(
                name: "IX_trendyolProductAttributes_TrendyolProductId",
                table: "trendyolProductAttributes",
                column: "TrendyolProductId");

            migrationBuilder.CreateIndex(
                name: "IX_trendyolProductImages_TrendyolProductId",
                table: "trendyolProductImages",
                column: "TrendyolProductId");

            migrationBuilder.CreateIndex(
                name: "IX_trendyolProducts_TrendyolBrandId",
                table: "trendyolProducts",
                column: "TrendyolBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_trendyolProducts_TrendyolCategoryId",
                table: "trendyolProducts",
                column: "TrendyolCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "contentBases");

            migrationBuilder.DropTable(
                name: "shopifyApiInfos");

            migrationBuilder.DropTable(
                name: "shopifyProductImages");

            migrationBuilder.DropTable(
                name: "shopifyProductOptions");

            migrationBuilder.DropTable(
                name: "shopifyVariants");

            migrationBuilder.DropTable(
                name: "symbols");

            migrationBuilder.DropTable(
                name: "trendyolApiInfos");

            migrationBuilder.DropTable(
                name: "trendyolProductAttributes");

            migrationBuilder.DropTable(
                name: "trendyolProductImages");

            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "pages");

            migrationBuilder.DropTable(
                name: "shopifyProducts");

            migrationBuilder.DropTable(
                name: "trendyolProducts");

            migrationBuilder.DropTable(
                name: "trendyolBrands");

            migrationBuilder.DropTable(
                name: "trendyolCategories");
        }
    }
}
