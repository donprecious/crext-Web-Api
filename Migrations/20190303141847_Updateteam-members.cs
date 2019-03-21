using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CrExtApiCore.Migrations
{
    public partial class Updateteammembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_TeamMembers_TeamMembers_TeamId",
            //    table: "TeamMembers");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Teams_TeamId",
                table: "TeamMembers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Teams_TeamId",
                table: "TeamMembers");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_TeamMembers_TeamId",
                table: "TeamMembers",
                column: "TeamId",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
