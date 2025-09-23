using Models;
using Models.DTO;

namespace Services;

public interface IAttractionService
{
   public Task<ResponsDTOItem<IAttraction>> ReadAttractionsAsync(Guid id, bool flat);
   public Task<List<IAttraction>> ReadAttraction(int number, bool comment);
   public Task<List<IAttraction>> ReadAttractionFilter(bool seeded,string filter);
}