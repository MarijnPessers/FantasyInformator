using System.ComponentModel.DataAnnotations;
using AutoFixture;
using FantasyInformator.State.Settings;

namespace FantasyInformator.State.Tests.Settings;

[TestClass]
public class StateSettingsTests
{
    private Fixture _fixture = null!;

    [TestInitialize]
    public void Initialize() =>
        _fixture = new Fixture();

    [TestMethod]
    public void Validate_ShouldBeValid_WhenFocusOwnerNameIsFilled()
    {
        var options = new StateSettings { FocusOwnerName = _fixture.Create<string>() };

        var results = Validate(options);

        Assert.IsEmpty(results);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    public void Validate_ShouldReportFocusOwnerName_WhenFocusOwnerNameIsBlank(string focusOwnerName)
    {
        var options = new StateSettings { FocusOwnerName = focusOwnerName };

        var results = Validate(options);

        Assert.HasCount(1, results);
        Assert.Contains(nameof(StateSettings.FocusOwnerName), results[0].MemberNames.ToList());
    }

    private static List<ValidationResult> Validate(StateSettings options)
    {
        var results = new List<ValidationResult>();
        _ = Validator.TryValidateObject(options, new ValidationContext(options), results, true);
        return results;
    }
}
