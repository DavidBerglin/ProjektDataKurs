using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Seido.Utilities.SeedGenerator;
using DbModels;
using DbContext;
using Configuration;
using Microsoft.VisualBasic;
using Models;

namespace DbRepos;

public class AdminDbRepos
{
    private const string _seedSource = "./app-seeds.json";
    private readonly ILogger<AdminDbRepos> _logger;
    private Encryptions _encryptions;
    private readonly MainDbContext _dbContext;

    public async Task SeedAsync(int number)
    {
        var seeder = new SeedGenerator();

        var address = seeder.ItemsToList<AddressDbM>(number);
        var users = seeder.ItemsToList<UsersDbM>(number);
        var attractions = seeder.ItemsToList<AttractionDbM>(number);
        // loopa över varje user och sätt en address till varje. 
        foreach (var user in users)
        {
            user.AddressDbM = seeder.Bool ? seeder.FromList(address) : null;
        }
        foreach (var attraction in attractions)
        {
            var AttractionAddress = seeder.FromList(address);
            attraction.AddressDbM = AttractionAddress;
        }

        await _dbContext.AttractionDbM.AddRangeAsync(attractions);
        await _dbContext.AddressDbM.AddRangeAsync(address);
        await _dbContext.UsersDbM.AddRangeAsync(users);
        await _dbContext.SaveChangesAsync();

        var comment = new List<CommentDbM>();
        foreach (var c in attractions)
        {
            int n = seeder.Next(0, 21);
            for (int i = 0; i < n; i++)
            {
                var u = seeder.FromList(users);
                comment.Add(new CommentDbM
                {
                    CommentId = Guid.NewGuid(),
                    Text = seeder.LatinSentence,
                    UserId = u.UserId,
                    AttractionId = c.AttractionId,
                });
            }
        }

        await _dbContext.CommentDbM.AddRangeAsync(comment);
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
