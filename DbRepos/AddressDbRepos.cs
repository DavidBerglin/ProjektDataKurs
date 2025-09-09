using Microsoft.EntityFrameworkCore;
using System.Data;

using Seido.Utilities.SeedGenerator;
using DbModels;
using DbContext;
using Configuration;
using Models;
using Microsoft.VisualBasic;
using DbRepos;

namespace DbRepos;

public class AddressDbRepos
{
    private readonly MainDbContext _dbContext;

    public async Task GetAddressAsync()
    {
        var seed = new SeedGenerator();
        _dbContext.AddressDbM.RemoveRange(_dbContext.AddressDbM);
        var address = seed.ItemsToList<AddressDbM>(1000);
        await _dbContext.AddressDbM.AddRangeAsync(address);
        await _dbContext.SaveChangesAsync();


    }
    public AddressDbRepos(MainDbContext context)
    {
        _dbContext = context;
    }
}
  
