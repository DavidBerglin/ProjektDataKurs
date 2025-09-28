using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

using Seido.Utilities.SeedGenerator;
using DbModels;
using DbContext;
using Configuration;
using Microsoft.VisualBasic;
using Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DbRepos;

public class AdminDbRepos
{
    private readonly ILogger<AdminDbRepos> _logger;
    private readonly MainDbContext _dbContext;

    public async Task SeedAsync(int number)
    {
        var seeder = new SeedGenerator();

        var address = seeder.ItemsToList<AddressDbM>(number);       
        var users = seeder.ItemsToList<UsersDbM>(number);           
        var attractions = seeder.ItemsToList<AttractionDbM>(number); 

        foreach (var user in users)
        {
            var userAddress = seeder.Bool ? seeder.FromList(address) : null;
            user.AddressDbM = userAddress;                    
            user.AddressId = userAddress?.AddressId;         
        }

        foreach (var attraction in attractions)
        {
            var AttractionAddress = seeder.FromList(address);
            attraction.AddressDbM = AttractionAddress;         
            attraction.AddressId = AttractionAddress.AddressId; 
            
            attraction.City = AttractionAddress.City;
            attraction.Country = AttractionAddress.Country;
        }
        
        await _dbContext.AddressDbM.AddRangeAsync(address);
        await _dbContext.AttractionDbM.AddRangeAsync(attractions);
        await _dbContext.UsersDbM.AddRangeAsync(users);
        await _dbContext.SaveChangesAsync(); 

        var savedAttractions = await _dbContext.AttractionDbM.ToListAsync();
        var savedUsers = await _dbContext.UsersDbM.ToListAsync();

        var comment = new List<CommentDbM>();
        foreach (var c in savedAttractions)
        {
            int n = seeder.Next(0, 21);
            for (int i = 0; i < n; i++)
            {
                var u = seeder.FromList(savedUsers);
                
                comment.Add(new CommentDbM
                {
                    Seeded = true,
                    CommentId = Guid.NewGuid(),
                    Text = seeder.Quote.Quote,              
                    UserId = u.UserId,                      
                    AttractionId = c.AttractionId         
                });
            }
        }

        await _dbContext.CommentDbM.AddRangeAsync(comment);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<string> RemoveAsync(bool seeded)
    {
        var stopwatch = Stopwatch.StartNew();

        _dbContext.CommentDbM.RemoveRange(_dbContext.CommentDbM.Where(a => a.Seeded == seeded));
        _dbContext.AttractionDbM.RemoveRange(_dbContext.AttractionDbM.Where(a => a.Seeded == seeded));
        _dbContext.AddressDbM.RemoveRange(_dbContext.AddressDbM.Where(a => a.Seeded == seeded));
        _dbContext.UsersDbM.RemoveRange(_dbContext.UsersDbM.Where(a => a.Seeded == seeded));
        stopwatch.Stop();
        var time = $"Removed in {stopwatch.Elapsed.TotalSeconds}seconds";
        await _dbContext.SaveChangesAsync();
        return time;

    }
    public async Task<string> RemoveSQL()
    {
        var stopwatch = Stopwatch.StartNew();
        await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM CommentDbM WHERE Seeded = 1");
        await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM AttractionDbM WHERE Seeded = 1");
        await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM AddressDbM WHERE Seeded = 1");
        await _dbContext.Database.ExecuteSqlRawAsync("DELETE FROM UsersDbM WHERE Seeded = 1");
        stopwatch.Stop();
        var timeSQL = $"Removed in {stopwatch.Elapsed.TotalSeconds}seconds";
        await _dbContext.SaveChangesAsync();
        return timeSQL;
        
    }

    public AdminDbRepos(ILogger<AdminDbRepos> logger, MainDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }
}
