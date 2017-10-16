using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HomERP.Domain.Migrations
{
    public partial class RemoveFamilyUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_FamilyUsers_FamilyUserId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "FamilyUsers");

            migrationBuilder.DropIndex(
                name: "IX_Payments_FamilyUserId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "FamilyUserId",
                table: "Payments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FamilyUserId",
                table: "Payments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FamilyUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_FamilyUserId",
                table: "Payments",
                column: "FamilyUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_FamilyUsers_FamilyUserId",
                table: "Payments",
                column: "FamilyUserId",
                principalTable: "FamilyUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
