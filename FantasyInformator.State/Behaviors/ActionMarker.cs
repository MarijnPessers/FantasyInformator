namespace FantasyInformator.State.Behaviors;

/// <summary>
/// The label that <see cref="TraceBehavior{TAction, TResponse}"/> puts on every activity it starts. It ends in a GUID
/// that is made anew each time the app starts, so nothing else can use the same label by accident. It lives outside
/// TraceBehavior because each filled-in version of TraceBehavior would get its own GUID.
/// </summary>
internal static class ActionMarker
{
    public static readonly string Key = $"FantasyInformator.Action.{Guid.NewGuid()}";
}
