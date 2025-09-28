using Microsoft.Extensions.Logging;

using DbRepos;

namespace Services;
    
public class AdminServiceDb : IAdminService
{
    private readonly AdminDbRepos _repo = null;
    private readonly ILogger<AdminServiceDb> _logger = null;

    public Task SeedAsync(int number) => _repo.SeedAsync(number);
    public Task <string>RemoveAsync(bool seeded) => _repo.RemoveAsync(seeded);

    public Task <string>RemoveSQL() => _repo.RemoveSQL();

    public AdminServiceDb(AdminDbRepos repo)
    {
        _repo = repo;
    }
    public AdminServiceDb(AdminDbRepos repo, ILogger<AdminServiceDb> logger):this(repo)
    {
        _logger = logger;
    }
}

