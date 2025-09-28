namespace Services;

public interface IAdminService
{
    public Task SeedAsync(int number);
    public Task <string>RemoveAsync(bool seeded);

    public Task <string>RemoveSQL();

    
}
