namespace FantasyInformator.Database.Mappers;

/// <summary>
/// The Map methods of <see cref="Mapper{TModel, TDatabaseModel}"/> need the model or database model as its own type.
/// But some code only has it as an object, because its type is only found while the program runs. So this interface
/// copies an object with any mapper, without naming its type.
/// Example: a PlayerDb that is only known as an object is copied into a Player.
/// </summary>
internal interface IMapper
{
    /// <summary>
    /// Copies a database model into its model, or a model into its database model, inside a mapping that is already
    /// running. Throws when this mapper does not copy that model or database model.
    /// </summary>
    object Map(object source, MapContext context);
}
