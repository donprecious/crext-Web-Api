using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrExtApiCore.Migrations
{
    public partial class relChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "ReviewNotificationsId",
                table: "Projects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ReviewNotificationsId",
                table: "Projects",
                column: "ReviewNotificationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ReviewNotifications_ReviewNotificationsId",
                table: "Projects",
                column: "ReviewNotificationsId",
                principalTable: "ReviewNotifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ReviewNotifications_ReviewNotificationsId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ReviewNotificationsId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ReviewNotificationsId",
                table: "Projects");

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
    }
}
