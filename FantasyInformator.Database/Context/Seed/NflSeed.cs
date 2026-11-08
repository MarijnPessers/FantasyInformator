using FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.Database.Context.Seed;

/// <summary>The 32 NFL teams. The external id is the Sleeper team code.</summary>
public static class NflSeed
{
    public static readonly IReadOnlyList<NflTeam> Teams =
    [
        Team("ARI", "Arizona Cardinals"),
        Team("ATL", "Atlanta Falcons"),
        Team("BAL", "Baltimore Ravens"),
        Team("BUF", "Buffalo Bills"),
        Team("CAR", "Carolina Panthers"),
        Team("CHI", "Chicago Bears"),
        Team("CIN", "Cincinnati Bengals"),
        Team("CLE", "Cleveland Browns"),
        Team("DAL", "Dallas Cowboys"),
        Team("DEN", "Denver Broncos"),
        Team("DET", "Detroit Lions"),
        Team("GB", "Green Bay Packers"),
        Team("HOU", "Houston Texans"),
        Team("IND", "Indianapolis Colts"),
        Team("JAX", "Jacksonville Jaguars"),
        Team("KC", "Kansas City Chiefs"),
        Team("LAC", "Los Angeles Chargers"),
        Team("LAR", "Los Angeles Rams"),
        Team("LV", "Las Vegas Raiders"),
        Team("MIA", "Miami Dolphins"),
        Team("MIN", "Minnesota Vikings"),
        Team("NE", "New England Patriots"),
        Team("NO", "New Orleans Saints"),
        Team("NYG", "New York Giants"),
        Team("NYJ", "New York Jets"),
        Team("PHI", "Philadelphia Eagles"),
        Team("PIT", "Pittsburgh Steelers"),
        Team("SEA", "Seattle Seahawks"),
        Team("SF", "San Francisco 49ers"),
        Team("TB", "Tampa Bay Buccaneers"),
        Team("TEN", "Tennessee Titans"),
        Team("WAS", "Washington Commanders"),
    ];

    private static NflTeam Team(string code, string name) =>
        new()
        {
            Id = SeedIds.For(nameof(NflTeam), code),
            ExternalId = code,
            Name = name,
            CreatedUtc = SeedIds.SeedDateUtc,
            UpdatedUtc = SeedIds.SeedDateUtc,
        };
}
