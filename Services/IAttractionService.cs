using Models;
using Models.DTO;

namespace Services;

public interface IAttractionService
{

   public Task<List<ReadAttractionSummaryDTO>> ReadAttraction(int number, bool comment);
   public Task<List<ReadAttractionsFilterDTO>> ReadAttractionFilter(bool seeded, string filter);
}