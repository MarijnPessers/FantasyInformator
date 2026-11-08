using System.ComponentModel.DataAnnotations;
using AutoFixture;
using FantasyInformator.Sleeper.Settings;

namespace FantasyInformator.Sleeper.Tests;

[TestClass]
public class SleeperSettingsTests
{
    private readonly Fixture _fixture = new();

    [TestMethod]
    public void Validate_UsernameFilled_IsValid()
    {
        var options = new SleeperSettings { Username = _fixture.Create<string>() };

        var results = Validate(options);

        Assert.IsEmpty(results);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    public void Validate_UsernameBlank_ReportsUsername(string username)
    {
        var options = new SleeperSettings { Username = username };

        var results = Validate(options);

        Assert.HasCount(1, results);
        Assert.Contains(nameof(SleeperSettings.Username), results[0].MemberNames.ToList());
    }

    private static List<ValidationResult> Validate(SleeperSettings options)
    {
        var results = new List<ValidationResult>();
        _ = Validator.TryValidateObject(options, new ValidationContext(options), results, true);
        return results;
    }
}
