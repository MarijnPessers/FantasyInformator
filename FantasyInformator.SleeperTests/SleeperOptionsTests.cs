using System.ComponentModel.DataAnnotations;
using AutoFixture;
using FantasyInformator.Sleeper;

namespace FantasyInformator.SleeperTests;

[TestClass]
public class SleeperOptionsTests
{
    private readonly Fixture _fixture = new();

    [TestMethod]
    public void Validate_UsernameFilled_IsValid()
    {
        var options = new SleeperOptions { Username = _fixture.Create<string>() };

        var results = Validate(options);

        Assert.IsEmpty(results);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    public void Validate_UsernameBlank_ReportsUsername(string username)
    {
        var options = new SleeperOptions { Username = username };

        var results = Validate(options);

        Assert.HasCount(1, results);
        CollectionAssert.Contains(results[0].MemberNames.ToList(), nameof(SleeperOptions.Username));
    }

    private static List<ValidationResult> Validate(SleeperOptions options)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(options, new ValidationContext(options), results, validateAllProperties: true);
        return results;
    }
}
