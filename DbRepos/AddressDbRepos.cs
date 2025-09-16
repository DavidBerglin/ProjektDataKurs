using Microsoft.EntityFrameworkCore;
using System.Data;

using Seido.Utilities.SeedGenerator;
using DbModels;
using DbContext;
using Configuration;
using Models;
using Microsoft.VisualBasic;
using DbRepos;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DbRepos;

public class AddressDbRepos
{
    private readonly MainDbContext _dbContext;

    public async Task<List<IAddress>> ReadAddressAsync(int number)
    {
         return await _dbContext.AddressDbM
        .AsNoTracking()
        .Take(number)
        .Cast<IAddress>()
        .ToListAsync();
      
    }
    public AddressDbRepos(MainDbContext context)
    {
        _dbContext = context;
    }
}
  
