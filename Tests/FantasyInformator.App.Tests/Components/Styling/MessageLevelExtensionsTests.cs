using FantasyInformator.App.Components.Styling;

namespace FantasyInformator.App.Tests.Components.Styling;

[TestClass]
public class MessageLevelExtensionsTests
{
    [TestMethod]
    [DataRow(MessageLevel.Debug, ColorVariant.Secondary)]
    [DataRow(MessageLevel.Information, ColorVariant.Info)]
    [DataRow(MessageLevel.Success, ColorVariant.Success)]
    [DataRow(MessageLevel.Warning, ColorVariant.Warning)]
    [DataRow(MessageLevel.Error, ColorVariant.Danger)]
    public void ToColorVariant_ShouldGiveTheColorOfTheLevel_WhenCalled(
        MessageLevel messageLevel,
        ColorVariant expectedColorVariant) =>
        Assert.AreEqual(expectedColorVariant, messageLevel.ToColorVariant());
}
