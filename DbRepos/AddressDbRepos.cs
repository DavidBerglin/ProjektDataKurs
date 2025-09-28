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
using Models.DTO;


namespace DbRepos;

public class AddressDbRepos
{
    private readonly MainDbContext _dbContext;

    public async Task<List<AddressDTO>> ReadAddressAsync(int number)
    {
        var addresses = await _dbContext.AddressDbM
            .AsNoTracking()
            .Include(c => c.UsersDbM)
            .Take(number)
            .ToListAsync();

        return addresses.Select(a => new AddressDTO
        {
            AddressId = a.AddressId,
            StreetAddress = a.StreetAddress,
            City = a.City,
            Country = a.Country
        }).ToList();
    }
    public AddressDbRepos(MainDbContext context)
    {
        _dbContext = context;
    }
}
  
