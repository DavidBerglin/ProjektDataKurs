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

    public async Task<List<IAttraction>>ReadAttractionAsync(int number)
    {
        return await _dbContext.AttractionDbM
        .AsNoTracking()
        .Include(a => a.AddressDbM)
        .Take(number)
        .Cast<IAttraction>()
        .ToListAsync();
    }

    public AttractionDbRepos(MainDbContext context)
    {
        _dbContext = context;
    }

}