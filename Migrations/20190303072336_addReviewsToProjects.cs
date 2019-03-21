using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrExtApiCore.Migrations
{
    public partial class addReviewsToProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectsId",
                table: "Reviews",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProjectsId",
                table: "Reviews",
                column: "ProjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Projects_ProjectsId",
                table: "Reviews",
                column: "ProjectsId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Projects_ProjectsId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ProjectsId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ProjectsId",
                table: "Reviews");
        }
    }
}
