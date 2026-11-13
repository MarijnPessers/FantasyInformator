using FantasyInformator.Database.Mappers.FantasyTeam;
using DatabaseFantasyTeam = FantasyInformator.Database.Models.FantasyTeam;

namespace FantasyInformator.Database.Tests.Mappers.FantasyTeam;

[TestClass]
public class OwnerMapperTests
{
    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var owner = SourceFactory.Create<DatabaseFantasyTeam.Owner>();

        var result = OwnerMapper.Map(owner);

        Assert.IsNotNull(result);
        MappedAssert.Equal(owner, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = SourceFactory.Create<DatabaseFantasyTeam.Owner>().Map();

        var result = model.Map();

        MappedAssert.Equal(result, model);
    }
}
