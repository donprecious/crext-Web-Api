using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrExtApiCore.Migrations
{
    public partial class newCustomersEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Customers",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Customers",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BalanceIssued",
                table: "Customers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Interest",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NUBAN_NUMBER",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OutstandingBalance",
                table: "Customers",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Recommendation",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "BalanceIssued",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Interest",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "NUBAN_NUMBER",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "OutstandingBalance",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Recommendation",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Customers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Customers",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
