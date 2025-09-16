namespace Services;

public interface IAttractionService
{
   public Task GetAttractionsAsync(int number);
}