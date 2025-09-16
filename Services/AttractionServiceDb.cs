using DbRepos;
namespace Services;

public class AttractionServiceDb : IAttractionService
{
    private readonly AttractionDbRepos _repo = null;

    public Task GetAttractionsAsync(int number)
    {
        return _repo.GetAttractionsAsync(number);
    }



    public AttractionServiceDb(AttractionDbRepos repo)
    {
        _repo = repo;
    }
}