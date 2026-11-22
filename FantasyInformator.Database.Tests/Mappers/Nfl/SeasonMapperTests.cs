using FantasyInformator.Database.Mappers.Nfl;
using FantasyInformator.Database.Models.Nfl;

namespace FantasyInformator.Database.Tests.Mappers.Nfl;

[TestClass]
public class SeasonMapperTests
{
    private SeasonMapper _mapper = null!;

    [TestInitialize]
    public void Initialize()
    {
        _mapper = new SeasonMapper();
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenDatabaseModelIsGiven()
    {
        var season = SourceFactory.Create<SeasonDb>();

        var result = _mapper.Map(season);

        Assert.IsNotNull(result);
        MappedAssert.Equal(season, result);
    }

    [TestMethod]
    public void Map_ShouldMapAllFieldsAndChildren_WhenApplicationModelIsGiven()
    {
        var model = _mapper.Map(SourceFactory.Create<SeasonDb>());

        var result = _mapper.Map(model);

        MappedAssert.Equal(result, model);
    }
}
