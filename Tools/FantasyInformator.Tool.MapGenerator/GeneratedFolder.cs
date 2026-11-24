namespace FantasyInformator.Tool.MapGenerator;

public static class GeneratedFolder
{
    /// <summary>
    /// Makes the folder hold exactly <paramref name="files"/>: deletes every other file in it, and writes a file only
    /// when its source changed, so an unchanged file keeps its date and the compiler can skip it.
    /// </summary>
    public static void Update(string folder, IReadOnlyDictionary<string, string> files)
    {
        _ = Directory.CreateDirectory(folder);

        foreach (var path in Directory.GetFiles(folder).Where(x => !files.ContainsKey(Path.GetFileName(x))))
        {
            File.Delete(path);
        }

        foreach (var (fileName, source) in files)
        {
            var path = Path.Combine(folder, fileName);

            if (!File.Exists(path) || File.ReadAllText(path) != source)
            {
                File.WriteAllText(path, source);
            }
        }
    }
}
