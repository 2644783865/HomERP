using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HomERP.Domain.Migrations
{
    public partial class AddContractorToPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContractorId",
                table: "Payments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Contractors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BuildingNumber = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false),
                    FamilyId = table.Column<int>(nullable: false),
                    LocalNumber = table.Column<string>(nullable: true),
                    NIP = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(maxLength: 100, nullable: false),
                    Street = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contractors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contractors_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Families",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ContractorId",
                table: "Payments",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Contractors_FamilyId",
                table: "Contractors",
                column: "FamilyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Contractors_ContractorId",
                table: "Payments",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Contractors_ContractorId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "Contractors");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ContractorId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "Payments");
        }
    }
}
