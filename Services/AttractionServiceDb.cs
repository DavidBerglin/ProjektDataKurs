using DbRepos;
using Models;
using Models.DTO;
namespace Services;

public class AttractionServiceDb : IAttractionService
{
    private readonly AttractionDbRepos _repo = null;

    public Task<ResponsDTOItem<IAttraction>> ReadAttractionsAsync(Guid id, bool flat) => _repo.ReadAttractionsAsync(id, flat);
    public Task<List<IAttraction>> ReadAttraction(int number) => _repo.ReadAttraction(number);

    public Task<List<IAttraction>> ReadAttractionNoComment() => _repo.ReadAttractionNoComment();
 

    public AttractionServiceDb(AttractionDbRepos repo)
    {
        _repo = repo;
    }
}