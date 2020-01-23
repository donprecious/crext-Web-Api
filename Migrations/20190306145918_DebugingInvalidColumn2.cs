using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrExtApiCore.Migrations
{
    public partial class DebugingInvalidColumn2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            //migrationBuilder.AddColumn<int>(
            //name: "ReviewNotificationsId",
            //table: "Projects",
            //nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Projects_ReviewNotificationsId",
            //    table: "Projects",
            //    column: "ReviewNotificationsId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Projects_ReviewNotifications_ReviewNotificationsId",
            //    table: "Projects",
            //    column: "ReviewNotificationsId",
            //    principalTable: "ReviewNotifications",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        
        migrationBuilder.DropForeignKey(
                name: "FK_Projects_ReviewNotifications_ReviewNotificationsId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ReviewNotificationsId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ReviewNotificationsId",
                table: "Projects");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewNotificationsId",
                table: "Projects",
                nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Projects_ReviewNotificationsId",
            //    table: "Projects",
            //    column: "ReviewNotificationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ReviewNotifications_ReviewNotificationsId",
                table: "Projects",
                column: "ReviewNotificationsId",
                principalTable: "ReviewNotifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
