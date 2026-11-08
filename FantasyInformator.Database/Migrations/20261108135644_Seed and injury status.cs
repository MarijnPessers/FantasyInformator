using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FantasyInformator.Database.Migrations
{
    /// <inheritdoc />
    public partial class Seedandinjurystatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDominant",
                table: "Positions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "InjuryStatus",
                table: "Players",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "NflTeams",
                columns: new[] { "Id", "CreatedUtc", "ExternalId", "Name", "UpdatedUtc" },
                values: new object[,]
                {
                    { new Guid("169426ab-d420-d2cf-d1e9-c302ac76592c"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "BUF", "Buffalo Bills", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("242371ef-bc00-541f-832b-db975a5b9311"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "KC", "Kansas City Chiefs", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2787fa35-e9fe-9b7b-5ef5-bb3928ba618d"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "PHI", "Philadelphia Eagles", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2c4c1f35-d0f5-e629-fa1b-3a23f7610463"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "DEN", "Denver Broncos", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("33478189-1435-7ace-1e0c-f557a05c55f2"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "NE", "New England Patriots", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("343573f7-de85-8448-c3ee-880ad11bd78b"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "NO", "New Orleans Saints", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("3fbaf877-50b2-3be0-9d50-4ffbaf85e84a"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "HOU", "Houston Texans", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("411a8286-7df2-733d-a1eb-d54937090910"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "TB", "Tampa Bay Buccaneers", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("438f8bff-fe7c-76f5-b9f9-4446c5e48e8f"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "MIN", "Minnesota Vikings", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("477cb81f-fce4-1aa3-6611-a7411d083987"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "ARI", "Arizona Cardinals", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4b518b62-c221-f330-33cb-8901703e72ea"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "LAC", "Los Angeles Chargers", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4f1b748a-68bc-a1f7-c3c4-ad1323fd1919"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "ATL", "Atlanta Falcons", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5133cb6d-60b5-1384-295f-ed192f8eca3b"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "LAR", "Los Angeles Rams", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("55625faa-6921-3ae6-23d2-1b3ed3bc9018"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "DET", "Detroit Lions", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("57292d6b-d97c-4b6a-952f-a1399bb5baf9"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "SF", "San Francisco 49ers", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5fa0875c-6d54-c10d-ebc8-45d59e79f128"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "BAL", "Baltimore Ravens", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("647b8649-1c20-23e3-b1d7-bcb66089e325"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "JAX", "Jacksonville Jaguars", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6bc0aabc-85f0-6ab7-7b71-03dd71cb271b"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "DAL", "Dallas Cowboys", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("713af161-dd28-a6da-305d-d8ff9be4a857"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "PIT", "Pittsburgh Steelers", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7392a2d3-e124-27e3-bd76-40cf83ae7796"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "GB", "Green Bay Packers", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7d3e68c4-0503-f0ac-f1b1-7b374d61e149"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "CHI", "Chicago Bears", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7dc440f5-d075-7bc1-692b-d1351807e87a"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "CIN", "Cincinnati Bengals", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9055ccd8-5b93-0975-4a6d-6a621bac18a6"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "NYG", "New York Giants", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a8bc70df-ccaa-aedc-a757-060acbf8e924"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "CAR", "Carolina Panthers", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b27961cb-8277-ed0e-eda8-2a439bd1b5b7"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "WAS", "Washington Commanders", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b686e7a7-cd16-c708-b3c8-b64706f448c4"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "NYJ", "New York Jets", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c10adeb6-5595-b6f4-f265-ed1b7ab405f7"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "IND", "Indianapolis Colts", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c2253ac4-cb37-9824-b5f8-3dd0b17fb378"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "MIA", "Miami Dolphins", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d7497b1c-826f-a6f9-bb1f-69c751b382cd"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "TEN", "Tennessee Titans", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e3c2c570-00ad-20ab-ce40-ae5c9baf2f65"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "LV", "Las Vegas Raiders", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ebdb04be-d832-9635-b77d-5c0a9eb867a0"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "CLE", "Cleveland Browns", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f4be765f-0f36-f6dc-1a7f-38065a6c4a36"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "SEA", "Seattle Seahawks", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Abbreviation", "CreatedUtc", "IsDominant", "Name", "UpdatedUtc" },
                values: new object[,]
                {
                    { new Guid("028b21cf-b36d-f6b3-b1de-d57c98c5f917"), "TE", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, "Tight end", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1abb9635-8274-1af0-3b69-c1e3bf32903e"), "S", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, "Safety", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("30a0c27b-5f4e-00cf-6615-2ba9673b6302"), "LB", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, "Linebacker", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("545a4686-65c2-45d5-5a60-242a060e5448"), "DL", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), true, "Defensive line", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5d674ca7-4266-8593-c9d8-4ee6ec5a2a60"), "DE", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), true, "Defensive end", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("87296e88-0968-e05a-237a-5f06232fb4a0"), "CB", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, "Cornerback", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b94a5a66-1b13-3c88-113d-60ed31b28272"), "K", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, "Kicker", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c35bf03c-c25c-eadb-eb97-3ef1887de9f7"), "WR", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, "Wide receiver", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e467be71-5d87-0e7f-755b-05dc53a8b0d5"), "RB", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, "Running back", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f7c41d6e-294a-20df-2451-b2b01b15fa29"), "DB", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, "Defensive back", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f904d4de-b1fb-a01f-10e9-8676f2f2a897"), "QB", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, "Quarterback", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Slots",
                columns: new[] { "Id", "Abbreviation", "CreatedUtc", "ExternalId", "Name", "UpdatedUtc" },
                values: new object[,]
                {
                    { new Guid("0cd13e02-0c26-1a21-4e79-66c05fa4ab2e"), "WR", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "WR", "Wide receiver", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("25120bdb-59a0-56a9-0a29-65d60a7ed594"), "IDP", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "IDP_FLEX", "IDP flex", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("546330eb-fdd5-8444-58a2-e099a25c1939"), "LB", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "LB", "Linebacker", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("6020052a-ffbe-c1ee-ed85-16e73c83df47"), "BN", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "BN", "Bench", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7b3a3679-4e8f-1e88-b501-b152cc67269b"), "SF", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "SUPER_FLEX", "Super flex", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7f6faf59-658e-4021-d4ac-30ead7cba6c4"), "FLEX", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "FLEX", "Flex", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("9cc8ee40-09bb-fdf7-8f67-d17c0495fb15"), "DL", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "DL", "Defensive line", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("af234767-0f40-64ab-7c96-ae1ba150ae81"), "TE", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "TE", "Tight end", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b2933926-cf2c-49bc-8d36-d06c60eb669c"), "K", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "K", "Kicker", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e54f0cc9-e342-babd-db6c-7f6374d37b05"), "QB", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "QB", "Quarterback", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e8ee82e1-07bf-8d49-523a-a550ff216a0f"), "TAX", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "TAX", "Taxi squad", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ed17b098-f232-4f48-3b78-9579af8290dd"), "IR", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "IR", "Injured reserve", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("fb4a5126-523d-2fc1-2d6e-606fcf379571"), "DB", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "DB", "Defensive back", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("fd45dc2e-27dd-d753-c32b-0283c26c3330"), "RB", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), "RB", "Running back", new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "SlotEligibilities",
                columns: new[] { "Id", "CreatedUtc", "IsPreferred", "PositionId", "SlotId", "UpdatedUtc" },
                values: new object[,]
                {
                    { new Guid("015ee0f3-5fa7-2bd0-6d1b-9b36000c5550"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("87296e88-0968-e05a-237a-5f06232fb4a0"), new Guid("25120bdb-59a0-56a9-0a29-65d60a7ed594"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("056cbffc-6fe4-b5af-c380-a94b75b9f506"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("f904d4de-b1fb-a01f-10e9-8676f2f2a897"), new Guid("e54f0cc9-e342-babd-db6c-7f6374d37b05"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0ea6e430-d4fa-ba12-01f5-611db397992d"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("87296e88-0968-e05a-237a-5f06232fb4a0"), new Guid("e8ee82e1-07bf-8d49-523a-a550ff216a0f"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("107bdb0d-e653-7448-529d-996934e40061"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("e467be71-5d87-0e7f-755b-05dc53a8b0d5"), new Guid("7b3a3679-4e8f-1e88-b501-b152cc67269b"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("114f5840-5f80-f76a-2823-b987337cec83"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("87296e88-0968-e05a-237a-5f06232fb4a0"), new Guid("6020052a-ffbe-c1ee-ed85-16e73c83df47"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("18798c25-4824-81db-9a05-fb362b627b36"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("1abb9635-8274-1af0-3b69-c1e3bf32903e"), new Guid("ed17b098-f232-4f48-3b78-9579af8290dd"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1901ec33-3138-fd97-7339-1deac354ec20"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("5d674ca7-4266-8593-c9d8-4ee6ec5a2a60"), new Guid("6020052a-ffbe-c1ee-ed85-16e73c83df47"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("1e5e8ba0-8248-f7ad-0cc7-1a0952989b0f"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("5d674ca7-4266-8593-c9d8-4ee6ec5a2a60"), new Guid("e8ee82e1-07bf-8d49-523a-a550ff216a0f"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("203fb2b2-bee9-faf3-18f3-6b20045f96dd"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("c35bf03c-c25c-eadb-eb97-3ef1887de9f7"), new Guid("0cd13e02-0c26-1a21-4e79-66c05fa4ab2e"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2110ccc8-6828-7a37-6687-043d5502793b"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("f7c41d6e-294a-20df-2451-b2b01b15fa29"), new Guid("6020052a-ffbe-c1ee-ed85-16e73c83df47"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("2b6a2302-0e2a-3473-b427-30e0a311f755"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("545a4686-65c2-45d5-5a60-242a060e5448"), new Guid("6020052a-ffbe-c1ee-ed85-16e73c83df47"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("349d6169-f573-e973-c80b-d478e1907968"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("c35bf03c-c25c-eadb-eb97-3ef1887de9f7"), new Guid("7b3a3679-4e8f-1e88-b501-b152cc67269b"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("36c883e2-e36e-67eb-9178-526b2b0570c8"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("c35bf03c-c25c-eadb-eb97-3ef1887de9f7"), new Guid("6020052a-ffbe-c1ee-ed85-16e73c83df47"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("41fe6dc0-23c6-9d46-d3ec-c9d3809c9a35"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("545a4686-65c2-45d5-5a60-242a060e5448"), new Guid("e8ee82e1-07bf-8d49-523a-a550ff216a0f"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("476b2f65-7276-56fa-93d9-224bcf5ecf57"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("e467be71-5d87-0e7f-755b-05dc53a8b0d5"), new Guid("fd45dc2e-27dd-d753-c32b-0283c26c3330"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("483796f6-ef9c-b0e6-4981-1cca5d620789"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("028b21cf-b36d-f6b3-b1de-d57c98c5f917"), new Guid("e8ee82e1-07bf-8d49-523a-a550ff216a0f"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("4b39ef1c-582e-e6bd-6d82-f9044de569a1"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("30a0c27b-5f4e-00cf-6615-2ba9673b6302"), new Guid("546330eb-fdd5-8444-58a2-e099a25c1939"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("5b2ec167-eb6c-93b4-aa50-00477af5e9b7"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("b94a5a66-1b13-3c88-113d-60ed31b28272"), new Guid("6020052a-ffbe-c1ee-ed85-16e73c83df47"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("63a5ae56-5e05-2032-c31f-841543f3d771"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("028b21cf-b36d-f6b3-b1de-d57c98c5f917"), new Guid("6020052a-ffbe-c1ee-ed85-16e73c83df47"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("67eabb07-0387-e97e-0a8a-5dbb6d3ded96"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("5d674ca7-4266-8593-c9d8-4ee6ec5a2a60"), new Guid("9cc8ee40-09bb-fdf7-8f67-d17c0495fb15"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("685a380e-7b09-8d59-0a23-831bb14a553e"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("028b21cf-b36d-f6b3-b1de-d57c98c5f917"), new Guid("af234767-0f40-64ab-7c96-ae1ba150ae81"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("75a447be-5570-9299-47f0-1e7de1ca84c9"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("028b21cf-b36d-f6b3-b1de-d57c98c5f917"), new Guid("7b3a3679-4e8f-1e88-b501-b152cc67269b"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("77b8ec46-1d69-785e-feea-159f9dddf367"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("545a4686-65c2-45d5-5a60-242a060e5448"), new Guid("25120bdb-59a0-56a9-0a29-65d60a7ed594"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7a1e0a22-1d1c-90ae-0f52-adbf6fd6949d"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("545a4686-65c2-45d5-5a60-242a060e5448"), new Guid("9cc8ee40-09bb-fdf7-8f67-d17c0495fb15"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("7c6da63a-2ef6-4d05-7e8c-859da559b5a2"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("e467be71-5d87-0e7f-755b-05dc53a8b0d5"), new Guid("e8ee82e1-07bf-8d49-523a-a550ff216a0f"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("80e9f465-a7e1-988d-93d0-2750e7f30de1"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("f904d4de-b1fb-a01f-10e9-8676f2f2a897"), new Guid("ed17b098-f232-4f48-3b78-9579af8290dd"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("81686bc7-5b8b-a044-f254-4f59328da560"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("f904d4de-b1fb-a01f-10e9-8676f2f2a897"), new Guid("e8ee82e1-07bf-8d49-523a-a550ff216a0f"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("87d2ead5-d88f-3b84-718f-13598370114d"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("028b21cf-b36d-f6b3-b1de-d57c98c5f917"), new Guid("7f6faf59-658e-4021-d4ac-30ead7cba6c4"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8858b299-0b1f-055d-a010-176621c36dfa"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("545a4686-65c2-45d5-5a60-242a060e5448"), new Guid("ed17b098-f232-4f48-3b78-9579af8290dd"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("887743b8-7ee5-3b75-6f23-cee47d3fc939"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("b94a5a66-1b13-3c88-113d-60ed31b28272"), new Guid("ed17b098-f232-4f48-3b78-9579af8290dd"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8dabd927-06f8-953d-b209-f76940c76b5e"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("30a0c27b-5f4e-00cf-6615-2ba9673b6302"), new Guid("e8ee82e1-07bf-8d49-523a-a550ff216a0f"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("8f0c0d08-ca02-d110-cb8a-b8c5f028f9d6"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("1abb9635-8274-1af0-3b69-c1e3bf32903e"), new Guid("6020052a-ffbe-c1ee-ed85-16e73c83df47"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("905b5336-6d0b-f3d7-f722-dbb941a12dc9"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("5d674ca7-4266-8593-c9d8-4ee6ec5a2a60"), new Guid("25120bdb-59a0-56a9-0a29-65d60a7ed594"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("90ec1645-bbe9-c3ba-984f-716f7ed983f3"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("f904d4de-b1fb-a01f-10e9-8676f2f2a897"), new Guid("7b3a3679-4e8f-1e88-b501-b152cc67269b"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("936f89f2-b28e-8d0d-7297-5ea4a042e361"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("1abb9635-8274-1af0-3b69-c1e3bf32903e"), new Guid("e8ee82e1-07bf-8d49-523a-a550ff216a0f"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("93c540f1-b6e3-830c-1a6a-cd84d9fbd5be"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("30a0c27b-5f4e-00cf-6615-2ba9673b6302"), new Guid("ed17b098-f232-4f48-3b78-9579af8290dd"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("94e70dbd-d2d5-41e1-8629-c5a902765e0e"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("30a0c27b-5f4e-00cf-6615-2ba9673b6302"), new Guid("25120bdb-59a0-56a9-0a29-65d60a7ed594"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a2b2bf60-2085-9a8f-232a-1de10807568b"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("e467be71-5d87-0e7f-755b-05dc53a8b0d5"), new Guid("7f6faf59-658e-4021-d4ac-30ead7cba6c4"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("a913ecaa-1b55-6eb7-07ea-644c55cee7af"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("1abb9635-8274-1af0-3b69-c1e3bf32903e"), new Guid("25120bdb-59a0-56a9-0a29-65d60a7ed594"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ae4e3fd0-3901-c733-5ad0-2ab473c16b09"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("e467be71-5d87-0e7f-755b-05dc53a8b0d5"), new Guid("ed17b098-f232-4f48-3b78-9579af8290dd"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("af2faf82-14b8-a7d5-92f8-e88a3cf357a3"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("5d674ca7-4266-8593-c9d8-4ee6ec5a2a60"), new Guid("ed17b098-f232-4f48-3b78-9579af8290dd"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("b1f85130-013d-9265-bbf5-b7babaca95be"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("028b21cf-b36d-f6b3-b1de-d57c98c5f917"), new Guid("ed17b098-f232-4f48-3b78-9579af8290dd"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("be513cbd-d713-0fa9-2195-d8b0b8aaac0d"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("87296e88-0968-e05a-237a-5f06232fb4a0"), new Guid("fb4a5126-523d-2fc1-2d6e-606fcf379571"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c08bf8fe-932d-550a-9f64-3554ad0db746"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("c35bf03c-c25c-eadb-eb97-3ef1887de9f7"), new Guid("7f6faf59-658e-4021-d4ac-30ead7cba6c4"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("c76c55b5-ea1b-8d8f-43f6-864dad302827"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("f7c41d6e-294a-20df-2451-b2b01b15fa29"), new Guid("e8ee82e1-07bf-8d49-523a-a550ff216a0f"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d37d63b5-d81c-186a-40d5-7e16219f12d0"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("c35bf03c-c25c-eadb-eb97-3ef1887de9f7"), new Guid("ed17b098-f232-4f48-3b78-9579af8290dd"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d6e87488-7c3e-fecb-1aaa-ce152cef0e24"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("b94a5a66-1b13-3c88-113d-60ed31b28272"), new Guid("b2933926-cf2c-49bc-8d36-d06c60eb669c"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("d748eb77-d29b-2510-0d2e-c72e1dbc9dfa"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("b94a5a66-1b13-3c88-113d-60ed31b28272"), new Guid("e8ee82e1-07bf-8d49-523a-a550ff216a0f"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("dbb63415-4fca-472c-41e8-bca76659331a"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("c35bf03c-c25c-eadb-eb97-3ef1887de9f7"), new Guid("e8ee82e1-07bf-8d49-523a-a550ff216a0f"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("df5d0b93-9dfb-6809-62ce-aa6c08ccc1bd"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("30a0c27b-5f4e-00cf-6615-2ba9673b6302"), new Guid("6020052a-ffbe-c1ee-ed85-16e73c83df47"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("e82f4dbe-05c4-c2ff-c72b-eb3c289177e5"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("f7c41d6e-294a-20df-2451-b2b01b15fa29"), new Guid("fb4a5126-523d-2fc1-2d6e-606fcf379571"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("efcdadfa-6752-f957-242e-1314a67ab578"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), true, new Guid("1abb9635-8274-1af0-3b69-c1e3bf32903e"), new Guid("fb4a5126-523d-2fc1-2d6e-606fcf379571"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f2220cc7-e864-c0fa-425c-9bef5d300b0c"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("f7c41d6e-294a-20df-2451-b2b01b15fa29"), new Guid("ed17b098-f232-4f48-3b78-9579af8290dd"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("f7eabc9a-cd2d-5070-92d2-a97f30307a6b"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("e467be71-5d87-0e7f-755b-05dc53a8b0d5"), new Guid("6020052a-ffbe-c1ee-ed85-16e73c83df47"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("fd79bbd1-ed04-3a1d-5932-631db8e81602"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("f7c41d6e-294a-20df-2451-b2b01b15fa29"), new Guid("25120bdb-59a0-56a9-0a29-65d60a7ed594"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ff892d74-03ab-6c4e-5e12-61a1fb6ac4a0"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("f904d4de-b1fb-a01f-10e9-8676f2f2a897"), new Guid("6020052a-ffbe-c1ee-ed85-16e73c83df47"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("ffd70120-d0ae-d697-4de0-6127cb9be80b"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("87296e88-0968-e05a-237a-5f06232fb4a0"), new Guid("ed17b098-f232-4f48-3b78-9579af8290dd"), new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("169426ab-d420-d2cf-d1e9-c302ac76592c"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("242371ef-bc00-541f-832b-db975a5b9311"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("2787fa35-e9fe-9b7b-5ef5-bb3928ba618d"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("2c4c1f35-d0f5-e629-fa1b-3a23f7610463"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("33478189-1435-7ace-1e0c-f557a05c55f2"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("343573f7-de85-8448-c3ee-880ad11bd78b"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("3fbaf877-50b2-3be0-9d50-4ffbaf85e84a"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("411a8286-7df2-733d-a1eb-d54937090910"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("438f8bff-fe7c-76f5-b9f9-4446c5e48e8f"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("477cb81f-fce4-1aa3-6611-a7411d083987"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("4b518b62-c221-f330-33cb-8901703e72ea"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("4f1b748a-68bc-a1f7-c3c4-ad1323fd1919"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("5133cb6d-60b5-1384-295f-ed192f8eca3b"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("55625faa-6921-3ae6-23d2-1b3ed3bc9018"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("57292d6b-d97c-4b6a-952f-a1399bb5baf9"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("5fa0875c-6d54-c10d-ebc8-45d59e79f128"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("647b8649-1c20-23e3-b1d7-bcb66089e325"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("6bc0aabc-85f0-6ab7-7b71-03dd71cb271b"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("713af161-dd28-a6da-305d-d8ff9be4a857"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("7392a2d3-e124-27e3-bd76-40cf83ae7796"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("7d3e68c4-0503-f0ac-f1b1-7b374d61e149"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("7dc440f5-d075-7bc1-692b-d1351807e87a"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("9055ccd8-5b93-0975-4a6d-6a621bac18a6"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("a8bc70df-ccaa-aedc-a757-060acbf8e924"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("b27961cb-8277-ed0e-eda8-2a439bd1b5b7"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("b686e7a7-cd16-c708-b3c8-b64706f448c4"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("c10adeb6-5595-b6f4-f265-ed1b7ab405f7"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("c2253ac4-cb37-9824-b5f8-3dd0b17fb378"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("d7497b1c-826f-a6f9-bb1f-69c751b382cd"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("e3c2c570-00ad-20ab-ce40-ae5c9baf2f65"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("ebdb04be-d832-9635-b77d-5c0a9eb867a0"));

            migrationBuilder.DeleteData(
                table: "NflTeams",
                keyColumn: "Id",
                keyValue: new Guid("f4be765f-0f36-f6dc-1a7f-38065a6c4a36"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("015ee0f3-5fa7-2bd0-6d1b-9b36000c5550"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("056cbffc-6fe4-b5af-c380-a94b75b9f506"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("0ea6e430-d4fa-ba12-01f5-611db397992d"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("107bdb0d-e653-7448-529d-996934e40061"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("114f5840-5f80-f76a-2823-b987337cec83"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("18798c25-4824-81db-9a05-fb362b627b36"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("1901ec33-3138-fd97-7339-1deac354ec20"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("1e5e8ba0-8248-f7ad-0cc7-1a0952989b0f"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("203fb2b2-bee9-faf3-18f3-6b20045f96dd"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("2110ccc8-6828-7a37-6687-043d5502793b"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("2b6a2302-0e2a-3473-b427-30e0a311f755"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("349d6169-f573-e973-c80b-d478e1907968"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("36c883e2-e36e-67eb-9178-526b2b0570c8"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("41fe6dc0-23c6-9d46-d3ec-c9d3809c9a35"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("476b2f65-7276-56fa-93d9-224bcf5ecf57"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("483796f6-ef9c-b0e6-4981-1cca5d620789"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("4b39ef1c-582e-e6bd-6d82-f9044de569a1"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("5b2ec167-eb6c-93b4-aa50-00477af5e9b7"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("63a5ae56-5e05-2032-c31f-841543f3d771"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("67eabb07-0387-e97e-0a8a-5dbb6d3ded96"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("685a380e-7b09-8d59-0a23-831bb14a553e"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("75a447be-5570-9299-47f0-1e7de1ca84c9"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("77b8ec46-1d69-785e-feea-159f9dddf367"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("7a1e0a22-1d1c-90ae-0f52-adbf6fd6949d"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("7c6da63a-2ef6-4d05-7e8c-859da559b5a2"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("80e9f465-a7e1-988d-93d0-2750e7f30de1"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("81686bc7-5b8b-a044-f254-4f59328da560"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("87d2ead5-d88f-3b84-718f-13598370114d"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("8858b299-0b1f-055d-a010-176621c36dfa"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("887743b8-7ee5-3b75-6f23-cee47d3fc939"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("8dabd927-06f8-953d-b209-f76940c76b5e"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("8f0c0d08-ca02-d110-cb8a-b8c5f028f9d6"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("905b5336-6d0b-f3d7-f722-dbb941a12dc9"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("90ec1645-bbe9-c3ba-984f-716f7ed983f3"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("936f89f2-b28e-8d0d-7297-5ea4a042e361"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("93c540f1-b6e3-830c-1a6a-cd84d9fbd5be"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("94e70dbd-d2d5-41e1-8629-c5a902765e0e"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("a2b2bf60-2085-9a8f-232a-1de10807568b"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("a913ecaa-1b55-6eb7-07ea-644c55cee7af"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("ae4e3fd0-3901-c733-5ad0-2ab473c16b09"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("af2faf82-14b8-a7d5-92f8-e88a3cf357a3"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("b1f85130-013d-9265-bbf5-b7babaca95be"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("be513cbd-d713-0fa9-2195-d8b0b8aaac0d"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("c08bf8fe-932d-550a-9f64-3554ad0db746"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("c76c55b5-ea1b-8d8f-43f6-864dad302827"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("d37d63b5-d81c-186a-40d5-7e16219f12d0"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("d6e87488-7c3e-fecb-1aaa-ce152cef0e24"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("d748eb77-d29b-2510-0d2e-c72e1dbc9dfa"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("dbb63415-4fca-472c-41e8-bca76659331a"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("df5d0b93-9dfb-6809-62ce-aa6c08ccc1bd"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("e82f4dbe-05c4-c2ff-c72b-eb3c289177e5"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("efcdadfa-6752-f957-242e-1314a67ab578"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("f2220cc7-e864-c0fa-425c-9bef5d300b0c"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("f7eabc9a-cd2d-5070-92d2-a97f30307a6b"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("fd79bbd1-ed04-3a1d-5932-631db8e81602"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("ff892d74-03ab-6c4e-5e12-61a1fb6ac4a0"));

            migrationBuilder.DeleteData(
                table: "SlotEligibilities",
                keyColumn: "Id",
                keyValue: new Guid("ffd70120-d0ae-d697-4de0-6127cb9be80b"));

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: new Guid("028b21cf-b36d-f6b3-b1de-d57c98c5f917"));

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: new Guid("1abb9635-8274-1af0-3b69-c1e3bf32903e"));

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: new Guid("30a0c27b-5f4e-00cf-6615-2ba9673b6302"));

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: new Guid("545a4686-65c2-45d5-5a60-242a060e5448"));

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: new Guid("5d674ca7-4266-8593-c9d8-4ee6ec5a2a60"));

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: new Guid("87296e88-0968-e05a-237a-5f06232fb4a0"));

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: new Guid("b94a5a66-1b13-3c88-113d-60ed31b28272"));

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: new Guid("c35bf03c-c25c-eadb-eb97-3ef1887de9f7"));

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: new Guid("e467be71-5d87-0e7f-755b-05dc53a8b0d5"));

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: new Guid("f7c41d6e-294a-20df-2451-b2b01b15fa29"));

            migrationBuilder.DeleteData(
                table: "Positions",
                keyColumn: "Id",
                keyValue: new Guid("f904d4de-b1fb-a01f-10e9-8676f2f2a897"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("0cd13e02-0c26-1a21-4e79-66c05fa4ab2e"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("25120bdb-59a0-56a9-0a29-65d60a7ed594"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("546330eb-fdd5-8444-58a2-e099a25c1939"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("6020052a-ffbe-c1ee-ed85-16e73c83df47"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("7b3a3679-4e8f-1e88-b501-b152cc67269b"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("7f6faf59-658e-4021-d4ac-30ead7cba6c4"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("9cc8ee40-09bb-fdf7-8f67-d17c0495fb15"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("af234767-0f40-64ab-7c96-ae1ba150ae81"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("b2933926-cf2c-49bc-8d36-d06c60eb669c"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("e54f0cc9-e342-babd-db6c-7f6374d37b05"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("e8ee82e1-07bf-8d49-523a-a550ff216a0f"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("ed17b098-f232-4f48-3b78-9579af8290dd"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("fb4a5126-523d-2fc1-2d6e-606fcf379571"));

            migrationBuilder.DeleteData(
                table: "Slots",
                keyColumn: "Id",
                keyValue: new Guid("fd45dc2e-27dd-d753-c32b-0283c26c3330"));

            migrationBuilder.DropColumn(
                name: "IsDominant",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "InjuryStatus",
                table: "Players");
        }
    }
}
