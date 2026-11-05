namespace FantasyInformator.Sleeper.Services.Models;

/// <summary>The user object of <c>GET /user/&lt;username&gt;</c>. Holds only the fields the application uses.</summary>
public class User
{
    public string UserId { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;
}
