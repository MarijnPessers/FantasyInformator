using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyInformator.Database.Migrations
{
    /// <inheritdoc />
    public partial class updatetracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "State",
                table: "SlotEligibilities");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Seasons");

            migrationBuilder.DropColumn(
                name: "State",
                table: "RosteredPlayers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "State",
                table: "PlayerSeasonStats");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "State",
                table: "PlayerPositions");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "State",
                table: "NflTeams");

            migrationBuilder.DropColumn(
                name: "State",
                table: "LeagueSlots");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "State",
                table: "LeagueInstances");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "State",
                table: "GameAppearances");

            migrationBuilder.DropColumn(
                name: "State",
                table: "FantasyTeams");

            migrationBuilder.DropColumn(
                name: "State",
                table: "FantasyScores");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "Slots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "SlotEligibilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "Seasons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "RosteredPlayers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "Positions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "PlayerSeasonStats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "Players",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "PlayerPositions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "Owners",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "NflTeams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "LeagueSlots",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "Leagues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "LeagueInstances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "Games",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "GameAppearances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "FantasyTeams",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtc",
                table: "FantasyScores",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "SlotEligibilities");

            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "Seasons");

            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "RosteredPlayers");

            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "PlayerSeasonStats");

            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "PlayerPositions");

            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "NflTeams");

            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "LeagueSlots");

            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "Leagues");

            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "LeagueInstances");

            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "GameAppearances");

            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "FantasyTeams");

            migrationBuilder.DropColumn(
                name: "CreatedUtc",
                table: "FantasyScores");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Slots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "SlotEligibilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Seasons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "RosteredPlayers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Positions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "PlayerSeasonStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "PlayerPositions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Owners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "NflTeams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "LeagueSlots",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Leagues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "LeagueInstances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "GameAppearances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "FantasyTeams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "FantasyScores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
