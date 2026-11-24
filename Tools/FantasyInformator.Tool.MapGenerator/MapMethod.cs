namespace FantasyInformator.Tool.MapGenerator;

public sealed record MapMethod(string Source, string Target)
{
    public string SourceName =>
        Source.Split('.')[^1];
}
