using FantasyInformator.App.Components.Styling;

namespace FantasyInformator.App.Tests.Components.Styling;

[TestClass]
public class ColorVariantExtensionsTests
{
    [TestMethod]
    [DataRow(ColorVariant.Primary, "bg-primary text-white")]
    [DataRow(ColorVariant.Warning, "bg-warning text-dark")]
    [DataRow(ColorVariant.Dark, "bg-dark text-white")]
    public void ToColorClasses_ShouldGiveTheBackgroundAndAReadableTextColor_WhenCalled(
        ColorVariant colorVariant,
        string expectedColorClasses) =>
        Assert.AreEqual(expectedColorClasses, colorVariant.ToColorClasses());

    [TestMethod]
    public void ToColorClasses_ShouldGiveClasses_WhenCalledForAnyColorVariant()
    {
        var colorVariantsWithoutClasses = Enum.GetValues<ColorVariant>()
            .Where(colorVariant => string.IsNullOrEmpty(colorVariant.ToColorClasses()))
            .ToList();

        Assert.IsEmpty(colorVariantsWithoutClasses);
    }
}
