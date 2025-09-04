namespace Services;

public interface IAttractionService
{
    public Task<List<string>> GetAttractionsAsync();
}