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

    public async Task GetAttractionsAsync(int number)
    {
        var g = new SeedGenerator();

        _dbContext.AttractionDbM.RemoveRange(_dbContext.AttractionDbM);
        var attractions = g.UniqueItemsToList<AttractionDbM>(number);
        await _dbContext.AttractionDbM.AddRangeAsync(attractions);
        await _dbContext.SaveChangesAsync();
    }

    public AttractionDbRepos(MainDbContext context)
    {
        _dbContext = context;
    }

}