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

public class UserDbRepos
{
    private readonly MainDbContext _dbContext;

    public async Task GetUsersAsync()
    {
        var seed = new SeedGenerator();
        _dbContext.UsersDbM.RemoveRange(_dbContext.UsersDbM);
        var users = seed.ItemsToList<UsersDbM>(1000);
        await _dbContext.UsersDbM.AddRangeAsync(users);
        await _dbContext.SaveChangesAsync();
    }
    public UserDbRepos(MainDbContext context)
    {
        _dbContext = context;
    }
}