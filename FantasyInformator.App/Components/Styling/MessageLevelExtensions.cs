using FantasyInformator.Shared.Progress;

namespace FantasyInformator.App.Components.Styling;

public static class MessageLevelExtensions
{
    extension(MessageLevel messageLevel)
    {
        /// <summary>
        /// The color that shows this level, for example <see cref="ColorVariant.Danger"/> for an error.
        /// </summary>
        public ColorVariant ToColorVariant() =>
            messageLevel switch
            {
                MessageLevel.Debug => ColorVariant.Secondary,
                MessageLevel.Information => ColorVariant.Info,
                MessageLevel.Success => ColorVariant.Success,
                MessageLevel.Warning => ColorVariant.Warning,
                MessageLevel.Error => ColorVariant.Danger,
                _ => throw new ArgumentOutOfRangeException(nameof(messageLevel), messageLevel, null),
            };
    }
}
