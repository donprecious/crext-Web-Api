using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrExtApiCore.Migrations
{
    public partial class addedTeamsToOrganisation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganisationId",
                table: "Teams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_OrganisationId",
                table: "Teams",
                column: "OrganisationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Organisations_OrganisationId",
                table: "Teams",
                column: "OrganisationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Organisations_OrganisationId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_OrganisationId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "Teams");
        }
    }
}
