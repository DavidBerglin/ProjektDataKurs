using DbRepos;
using Models;
using Models.DTO;
namespace Services;

public class AttractionServiceDb : IAttractionService
{
    private readonly AttractionDbRepos _repo = null;

    public Task<ResponsDTOItem<IAttraction>> ReadAttractionsAsync(Guid id, bool flat) => _repo.ReadAttractionsAsync(id, flat);
    public Task<List<IAttraction>> ReadAttraction(int number, bool comment) => _repo.ReadAttraction(number,comment);

    public Task<List<IAttraction>> ReadAttractionFilter(bool seeded,string filter) => _repo.ReadAttractionFilter(seeded,filter);
 

    public AttractionServiceDb(AttractionDbRepos repo)
    {
        _repo = repo;
    }
}