using Microsoft.EntityFrameworkCore;
using System.Data;

using Seido.Utilities.SeedGenerator;
using DbModels;
using DbContext;
using Configuration;
using Models;
using Microsoft.VisualBasic;
using DbRepos;
using System.IO.Compression;

namespace DbRepos;

public class UserDbRepos
{
    private readonly MainDbContext _dbContext;

    public async Task<List<IUsers>> ReadUsersAsync(int number)
    {
        return await _dbContext.UsersDbM
        .AsNoTracking()
        .Include(a => a.AddressDbM)
        .Take(number)
        .Cast<IUsers>()
        .ToListAsync();
        
    }
    public UserDbRepos(MainDbContext context)
    {
        _dbContext = context;
    }
}