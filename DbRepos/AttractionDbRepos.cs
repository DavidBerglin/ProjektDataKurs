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

public class AttractionDbRepos
{
    private readonly MainDbContext _dbContext;

    public async Task GetAttractionsAsync()
    {
        var seeder = new SeedGenerator();

        _dbContext.AttractionDbM.RemoveRange(_dbContext.AttractionDbM);

        var attractions = seeder.ItemsToList<AttractionDbM>(1000);
        _dbContext.AttractionDbM.AddRange(attractions);

        await _dbContext.SaveChangesAsync();
    }

    public AttractionDbRepos(MainDbContext context)
    {
        _dbContext = context;
    }

}