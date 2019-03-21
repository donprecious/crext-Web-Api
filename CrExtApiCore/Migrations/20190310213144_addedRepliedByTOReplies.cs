using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrExtApiCore.Migrations
{
    public partial class addedRepliedByTOReplies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Repliedby",
                table: "Replies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_Repliedby",
                table: "Replies",
                column: "Repliedby");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_AspNetUsers_Repliedby",
                table: "Replies",
                column: "Repliedby",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_AspNetUsers_Repliedby",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_Repliedby",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "Repliedby",
                table: "Replies");
        }
    }
}
