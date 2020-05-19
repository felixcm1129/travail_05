using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BillingManagement.UI.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    PicturePath = table.Column<string>(nullable: true),
                    ContactInfo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfos",
                columns: table => new
                {
                    ContactInfoID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContactType = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    CustomerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.ContactInfoID);
                    table.ForeignKey(
                        name: "FK_ContactInfos_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreationDateTime = table.Column<DateTime>(nullable: false),
                    CustomerID = table.Column<int>(nullable: true),
                    SubTotal = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfos_CustomerID",
                table: "ContactInfos",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerID",
                table: "Invoices",
                column: "CustomerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInfos");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
