namespace FantasyInformator.Sleeper.Services.Models;

/// <summary>The <c>metadata</c> object of a league user. The Sleeper docs only show <c>team_name</c>.</summary>
public class LeagueUserMetadata
{
    /// <summary>The name the user gave the team. Null when the user never set one.</summary>
    public string? TeamName { get; set; }
}
