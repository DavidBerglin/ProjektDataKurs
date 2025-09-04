using Microsoft.EntityFrameworkCore;
using System.Data;

using Seido.Utilities.SeedGenerator;
using DbModels;
using DbContext;
using Configuration;
using models;
using Microsoft.VisualBasic;

namespace DbRepos;

public class AttractionDbRepos
{
    private const string _seedSource = "./app-seeds.json";

    private readonly MainDbContext _dbContext;

    public async Task GetAttractionsAsync()
    {
        var fn = Path.GetFullPath(_seedSource);
        var seeder = new SeedGenerator(fn);

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