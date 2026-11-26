using System.ComponentModel.DataAnnotations;

namespace FantasyInformator.State.Settings;

public class StateSettings
{
    public const string SectionName = "State";

    [Required]
    public string FocusOwnerName { get; set; } = string.Empty;
}
