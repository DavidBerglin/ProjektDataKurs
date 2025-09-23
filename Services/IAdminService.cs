namespace Services;

public interface IAdminService
{
    public Task SeedAsync(int number);
    public Task RemoveAsync(bool seeded);

    
}
