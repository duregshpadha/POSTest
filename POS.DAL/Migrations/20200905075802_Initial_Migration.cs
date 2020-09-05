using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace POS.DAL.Migrations
{
    public partial class Initial_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "MasterCustomer",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 42, nullable: false),
                    Phone = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterCustomer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterItem",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 42, nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "POSMain",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 42, nullable: false),
                    PosDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<string>(maxLength: 42, nullable: false),
                    TotalQuantity = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSMain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POSMain_MasterCustomer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "dbo",
                        principalTable: "MasterCustomer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "POSDetail",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 42, nullable: false),
                    POSMainID = table.Column<string>(maxLength: 42, nullable: false),
                    ItemId = table.Column<string>(maxLength: 42, nullable: false),
                    SaleOrReturn = table.Column<string>(maxLength: 1, nullable: false),
                    ItemQuantity = table.Column<int>(nullable: false),
                    ItemRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POSDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_POSDetail_MasterItem_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "dbo",
                        principalTable: "MasterItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POSDetail_POSMain_POSMainID",
                        column: x => x.POSMainID,
                        principalSchema: "dbo",
                        principalTable: "POSMain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "MasterCustomer",
                columns: new[] { "Id", "Name", "Phone" },
                values: new object[] { "09052020-074005322-f7390afc-c276-415f-9a20", "Ram", "1234567890" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "MasterItem",
                columns: new[] { "Id", "Name", "Rate", "Stock" },
                values: new object[] { "09052020-073914277-53924f75-cae5-475b-a5eb", "Rice", 10m, 10 });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "POSMain",
                columns: new[] { "Id", "CustomerId", "PosDate", "TotalAmount", "TotalQuantity" },
                values: new object[] { "09052020-074037271-6be8d386-4ad5-4e92-b827", "09052020-074005322-f7390afc-c276-415f-9a20", new DateTime(2020, 9, 5, 13, 28, 1, 921, DateTimeKind.Local).AddTicks(2564), 10m, 1 });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "POSDetail",
                columns: new[] { "Id", "ItemId", "ItemQuantity", "ItemRate", "POSMainID", "SaleOrReturn", "TotalAmount" },
                values: new object[] { "09052020-073953202-81be9799-750d-4955-bb17", "09052020-073914277-53924f75-cae5-475b-a5eb", 1, 10m, "09052020-074037271-6be8d386-4ad5-4e92-b827", "S", 10m });

            migrationBuilder.CreateIndex(
                name: "IX_MasterCustomer_Phone",
                schema: "dbo",
                table: "MasterCustomer",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_POSDetail_ItemId",
                schema: "dbo",
                table: "POSDetail",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_POSDetail_POSMainID",
                schema: "dbo",
                table: "POSDetail",
                column: "POSMainID");

            migrationBuilder.CreateIndex(
                name: "IX_POSMain_CustomerId",
                schema: "dbo",
                table: "POSMain",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "POSDetail",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MasterItem",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "POSMain",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "MasterCustomer",
                schema: "dbo");
        }
    }
}
