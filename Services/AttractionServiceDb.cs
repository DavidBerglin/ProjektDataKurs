using DbRepos;
namespace Services;

public class AttractionServiceDb : IAttractionService
{
    private readonly AttractionDbRepos _repo = null;

    public Task GetAttractionsAsync(int count = 500, CancellationToken ct = default)
    {
        return _repo.GetAttractionsAsync(count, ct);
    }



    public AttractionServiceDb(AttractionDbRepos repo)
    {
        _repo = repo;
    }
}