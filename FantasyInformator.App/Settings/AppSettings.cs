using System.ComponentModel.DataAnnotations;

namespace FantasyInformator.App.Settings;

public class AppSettings
{
    public const string SectionName = "App";

    [Required]
    public string OwnerName { get; set; } = string.Empty;
}
