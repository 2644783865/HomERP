using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HomERP.Domain.Migrations
{
    public partial class changeondeleterestrictions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashAccounts_Families_FamilyId",
                table: "CashAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contractors_Families_FamilyId",
                table: "Contractors");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_CashAccounts_CashAccountId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Contractors_ContractorId",
                table: "Payments");

            migrationBuilder.AddForeignKey(
                name: "FK_CashAccounts_Families_FamilyId",
                table: "CashAccounts",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contractors_Families_FamilyId",
                table: "Contractors",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_CashAccounts_CashAccountId",
                table: "Payments",
                column: "CashAccountId",
                principalTable: "CashAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Contractors_ContractorId",
                table: "Payments",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashAccounts_Families_FamilyId",
                table: "CashAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contractors_Families_FamilyId",
                table: "Contractors");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_CashAccounts_CashAccountId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Contractors_ContractorId",
                table: "Payments");

            migrationBuilder.AddForeignKey(
                name: "FK_CashAccounts_Families_FamilyId",
                table: "CashAccounts",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contractors_Families_FamilyId",
                table: "Contractors",
                column: "FamilyId",
                principalTable: "Families",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_CashAccounts_CashAccountId",
                table: "Payments",
                column: "CashAccountId",
                principalTable: "CashAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Contractors_ContractorId",
                table: "Payments",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
