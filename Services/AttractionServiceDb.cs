
namespace Services;

public class AttractionServiceDb : IAttractionService
{
    private readonly AttractionDbRepos _repo = null;

    public Task<List<string>> GetAttractionsAsync() => _repo.GetAttractionsAsync();

    public AttractionServiceDb(AttractionDbRepos repo)
    {
        _repo = repo;
    }
}