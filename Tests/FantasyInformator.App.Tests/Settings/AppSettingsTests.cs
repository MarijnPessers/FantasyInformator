using System.ComponentModel.DataAnnotations;
using AutoFixture;
using FantasyInformator.App.Settings;

namespace FantasyInformator.App.Tests.Settings;

[TestClass]
public class AppSettingsTests
{
    private Fixture _fixture = null!;

    [TestInitialize]
    public void Initialize() =>
        _fixture = new Fixture();

    [TestMethod]
    public void Validate_ShouldBeValid_WhenOwnerNameIsFilled()
    {
        var options = new AppSettings { OwnerName = _fixture.Create<string>() };

        var results = Validate(options);

        Assert.IsEmpty(results);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    public void Validate_ShouldReportOwnerName_WhenOwnerNameIsBlank(string ownerName)
    {
        var options = new AppSettings { OwnerName = ownerName };

        var results = Validate(options);

        Assert.HasCount(1, results);
        Assert.Contains(nameof(AppSettings.OwnerName), results[0].MemberNames.ToList());
    }

    private static List<ValidationResult> Validate(AppSettings options)
    {
        var results = new List<ValidationResult>();
        _ = Validator.TryValidateObject(options, new ValidationContext(options), results, true);
        return results;
    }
}
