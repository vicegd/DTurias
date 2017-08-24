using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DTuriasData.Migrations
{
    public partial class _15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CoordinatesId",
                table: "Tweets",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Favorited",
                table: "Tweets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FullText",
                table: "Tweets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRetweet",
                table: "Tweets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTweetDestroyed",
                table: "Tweets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTweetPublished",
                table: "Tweets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "QuotedTweet",
                table: "Tweets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "Retweeted",
                table: "Tweets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "RetweetedTweet",
                table: "Tweets",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Tweets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CoordinateModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoordinateModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HashTagModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TweetModelId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HashTagModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HashTagModel_Tweets_TweetModelId",
                        column: x => x.TweetModelId,
                        principalTable: "Tweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tweets_CoordinatesId",
                table: "Tweets",
                column: "CoordinatesId");

            migrationBuilder.CreateIndex(
                name: "IX_HashTagModel_TweetModelId",
                table: "HashTagModel",
                column: "TweetModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tweets_CoordinateModel_CoordinatesId",
                table: "Tweets",
                column: "CoordinatesId",
                principalTable: "CoordinateModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tweets_CoordinateModel_CoordinatesId",
                table: "Tweets");

            migrationBuilder.DropTable(
                name: "CoordinateModel");

            migrationBuilder.DropTable(
                name: "HashTagModel");

            migrationBuilder.DropIndex(
                name: "IX_Tweets_CoordinatesId",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "CoordinatesId",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "Favorited",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "FullText",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "IsRetweet",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "IsTweetDestroyed",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "IsTweetPublished",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "QuotedTweet",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "Retweeted",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "RetweetedTweet",
                table: "Tweets");

            migrationBuilder.DropColumn(
                name: "Source",
                table: "Tweets");
        }
    }
}
