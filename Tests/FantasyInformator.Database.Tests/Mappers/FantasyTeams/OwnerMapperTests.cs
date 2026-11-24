using FantasyInformator.Database.Mappers.FantasyTeams;
using FantasyInformator.Database.Models.FantasyTeams;

namespace FantasyInformator.Database.Tests.Mappers.FantasyTeams;

[TestClass]
public class OwnerMapperTests
{
    private OwnerMapper _mapper = null!;

    [TestInitialize]
    public void Initialize() => _mapper = new OwnerMapper();

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var owner = SourceFactory.Create<OwnerDb>();

        var result = _mapper.Map(owner);

        Assert.IsNotNull(result);
        MappedAssert.Equal(owner, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = _mapper.Map(SourceFactory.Create<OwnerDb>());

        var result = _mapper.Map(model);

        MappedAssert.Equal(result, model);
    }
}
