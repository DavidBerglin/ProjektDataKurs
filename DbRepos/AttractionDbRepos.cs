using Microsoft.EntityFrameworkCore;
using System.Data;

using Seido.Utilities.SeedGenerator;
using DbModels;
using DbContext;
using Configuration;
using Models;
using Microsoft.VisualBasic;
using DbRepos;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Models.DTO;
using Microsoft.AspNetCore.Http.Features;

namespace DbRepos;

public class AttractionDbRepos
{
    private readonly MainDbContext _dbContext;

    // Repository returnerar interface via projection
    public async Task<ResponsDTOItem<IAttraction>> ReadAttractionsAsync(Guid id, bool flat)
    {
        if (!flat)
        {
            var nonFlatQuery = _dbContext.AttractionDbM.AsNoTracking()
            .Include(c => c.CommentDbM)
            .Where(c => c.AttractionId == id);
            return new ResponsDTOItem<IAttraction>()
            {
                Item = await nonFlatQuery.FirstOrDefaultAsync<IAttraction>()
            };
        }
        else
        {
            var flatQuery = _dbContext.AttractionDbM.AsNoTracking()
            .Where(i => i.AttractionId == id);
             return new ResponsDTOItem<IAttraction>()
            {
                Item = await flatQuery.FirstOrDefaultAsync<IAttraction>()
            };
        }
        
    } 
     public async Task<List<IAttraction>>ReadAttraction(int number)
    {
        return await _dbContext.AttractionDbM
        .AsNoTracking()
        .Include(a => a.AddressDbM)
        .Include(a => a.CommentDbM)
            .ThenInclude(c => c.UsersDbM)
        .Take(number)
        .Cast<IAttraction>()
        .ToListAsync();
    }
     public async Task<List<IAttraction>>ReadAttractionNoComment()
    {
        return await _dbContext.AttractionDbM
        .AsNoTracking()
        .Include(a => a.AddressDbM)
        .Include(a => a.CommentDbM)
            .ThenInclude(c => c.UsersDbM)
        .Where(i => !i.CommentDbM.Any())
        .Cast<IAttraction>()
        .ToListAsync();
    }

  

    public AttractionDbRepos(MainDbContext context)
    {
        _dbContext = context;
    }
}