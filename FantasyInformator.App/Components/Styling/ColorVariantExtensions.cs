namespace FantasyInformator.App.Components.Styling;

public static class ColorVariantExtensions
{
    extension(ColorVariant colorVariant)
    {
        /// <summary>
        /// The Tailwind classes for the background color and a text color that is readable on it, for example
        /// <c>"bg-warning text-dark"</c> for <see cref="ColorVariant.Warning"/>.
        /// </summary>
        public string ToColorClasses() =>
            colorVariant switch
            {
                ColorVariant.Primary => "bg-primary text-white",
                ColorVariant.Secondary => "bg-secondary text-white",
                ColorVariant.Success => "bg-success text-white",
                ColorVariant.Danger => "bg-danger text-white",
                ColorVariant.Warning => "bg-warning text-dark",
                ColorVariant.Info => "bg-info text-dark",
                ColorVariant.Light => "bg-light text-dark",
                ColorVariant.Dark => "bg-dark text-white",
                _ => throw new ArgumentOutOfRangeException(nameof(colorVariant), colorVariant, null),
            };
    }
}
