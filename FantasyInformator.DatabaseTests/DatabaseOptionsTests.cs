using System.ComponentModel.DataAnnotations;
using AutoFixture;
using FantasyInformator.Database;

namespace FantasyInformator.DatabaseTests;

[TestClass]
public class DatabaseOptionsTests
{
    private readonly Fixture _fixture = new();

    [TestMethod]
    public void Validate_ConnectionStringAppFilled_IsValid()
    {
        var options = new DatabaseOptions { ConnectionStringApp = _fixture.Create<string>() };

        var results = Validate(options);

        Assert.IsEmpty(results);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    public void Validate_ConnectionStringAppBlank_ReportsConnectionStringApp(string connectionString)
    {
        var options = new DatabaseOptions { ConnectionStringApp = connectionString };

        var results = Validate(options);

        Assert.HasCount(1, results);
        Assert.Contains(nameof(DatabaseOptions.ConnectionStringApp), results[0].MemberNames.ToList());
    }

    [TestMethod]
    public void Validate_ConnectionStringEFBlank_IsValid()
    {
        var options = new DatabaseOptions
        {
            ConnectionStringApp = _fixture.Create<string>(),
            ConnectionStringEF = string.Empty,
        };

        var results = Validate(options);

        Assert.IsEmpty(results);
    }

    private static List<ValidationResult> Validate(DatabaseOptions options)
    {
        var results = new List<ValidationResult>();
        _ = Validator.TryValidateObject(options, new ValidationContext(options), results, true);
        return results;
    }
}
