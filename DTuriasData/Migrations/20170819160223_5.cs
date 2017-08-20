using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DTuriasData.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AuthorId",
                table: "Tweets",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tweets_AuthorId",
                table: "Tweets",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tweets_Users_AuthorId",
                table: "Tweets",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweets_Users_AuthorId",
                table: "Tweets");

            migrationBuilder.DropIndex(
                name: "IX_Tweets_AuthorId",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Tweets");
        }
    }
}
