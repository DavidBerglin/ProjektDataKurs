using DbRepos;
using Models;
namespace Services;

public class AttractionServiceDb : IAttractionService
{
    private readonly AttractionDbRepos _repo = null;

    public Task<List<IAttraction>> ReadAttractionAsync(int number)
    {
        return _repo.ReadAttractionAsync(number);
    }



    public AttractionServiceDb(AttractionDbRepos repo)
    {
        _repo = repo;
    }
}