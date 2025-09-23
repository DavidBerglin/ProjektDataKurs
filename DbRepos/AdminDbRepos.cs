using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
    private Encryptions _encryptions;
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
    public async Task RemoveAsync(bool seeded)
    {
        _dbContext.CommentDbM.RemoveRange(_dbContext.CommentDbM.Where(a => a.Seeded == seeded));
        _dbContext.AttractionDbM.RemoveRange(_dbContext.AttractionDbM.Where(a => a.Seeded == seeded));
        _dbContext.AddressDbM.RemoveRange(_dbContext.AddressDbM.Where(a => a.Seeded == seeded));
        _dbContext.UsersDbM.RemoveRange(_dbContext.UsersDbM.Where(a => a.Seeded == seeded));

        await _dbContext.SaveChangesAsync();

        



    }

    public AdminDbRepos(ILogger<AdminDbRepos> logger, Encryptions encryptions, MainDbContext context)
    {
        _logger = logger;
        _encryptions = encryptions;
        _dbContext = context;
    }
}
