using Models;

namespace Services;

public interface IAttractionService
{
   public Task<List<IAttraction>> ReadAttractionAsync(int number);
}