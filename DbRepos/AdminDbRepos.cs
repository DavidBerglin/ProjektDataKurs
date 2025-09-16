using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Seido.Utilities.SeedGenerator;
using DbModels;
using DbContext;
using Configuration;
using Microsoft.VisualBasic;

namespace DbRepos;

public class AdminDbRepos
{
    private const string _seedSource = "./app-seeds.json";
    private readonly ILogger<AdminDbRepos> _logger;
    private Encryptions _encryptions;
    private readonly MainDbContext _dbContext;

    public async Task SeedAsync(int number)
    {
        //Create a seeder
        var seeder = new SeedGenerator();

        var address = seeder.ItemsToList<AddressDbM>(number);
        var users = seeder.ItemsToList<UsersDbM>(number);
        var attractions = seeder.ItemsToList<AttractionDbM>(number);
        var comments = seeder.ItemsToList<CommentDbM>(number);

        foreach (var user in users)
        {
            user.AddressDbM = (seeder.Bool) ? seeder.FromList(address) : null;
        }

        await _dbContext.AttractionDbM.AddRangeAsync(attractions);
        await _dbContext.AddressDbM.AddRangeAsync(address);
        await _dbContext.UsersDbM.AddRangeAsync(users);
        



        //Save changes to the database
        await _dbContext.SaveChangesAsync();
    }

/*    public async Task RemoveCreditCard(CancellationToken ct = default)
    {
        await _dbContext.CreditCardDbM.ExecuteDeleteAsync(ct);
    }
*/
    public AdminDbRepos(ILogger<AdminDbRepos> logger, Encryptions encryptions, MainDbContext context)
    {
        _logger = logger;
        _encryptions = encryptions;
        _dbContext = context;
    }
}
