using System.ComponentModel.DataAnnotations;

namespace FantasyInformator.State.Settings;

public class StateSettings
{
    public const string SectionName = "State";

    [Required]
    public string OwnerName { get; set; } = string.Empty;
}
