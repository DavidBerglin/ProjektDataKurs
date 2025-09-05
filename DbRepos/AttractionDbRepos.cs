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

    public async Task GetAttractionsAsync(int count = 500, CancellationToken ct = default)
    {
        var g = new SeedGenerator();

        await _dbContext.AttractionDbM.ExecuteDeleteAsync(ct);

        var attractions = Enumerable.Range(0, count).Select(i => new AttractionDbM
        {
            Id = Guid.NewGuid(),
            Name = $"{g.FromString("Zoo,Museum,Park,Castle")} {g.City()} #{i}",
            City = g.City(),
            Country = g.Country,
            Street = g.StreetAddress(),
            Zip = g.ZipCode
        }).ToList();

        await _dbContext.AttractionDbM.AddRangeAsync(attractions, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public AttractionDbRepos(MainDbContext context)
    {
        _dbContext = context;
    }

}