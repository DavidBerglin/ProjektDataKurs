namespace Services;

public interface IAttractionService
{
   public Task GetAttractionsAsync(int count = 500, CancellationToken ct = default);
}