using System.Reflection;
using FantasyInformator.Tool.MapGenerator;

try
{
    var arguments = MapGeneratorArguments.Parse(args);
    var mapMethods = MapMethods.Find(Assembly.LoadFrom(arguments.ModelsAssemblyPath), arguments);

    GeneratedFolder.Update(arguments.OutputFolder, GeneratedFiles.Create(mapMethods, arguments.ExtensionsNamespace));

    return 0;
}
catch (MapGeneratorException exception)
{
    await Console.Error.WriteLineAsync(exception.Message);

    return 1;
}
