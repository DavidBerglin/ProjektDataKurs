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
using Microsoft.AspNetCore.Mvc;

namespace DbRepos;

public class AttractionDbRepos
{
    private readonly MainDbContext _dbContext;

    // Repository returnerar interface via projection
    public async Task<List<ReadAttractionSummaryDTO>> ReadAttraction(int number, bool comment)
    {
        if (comment)
        {
            return await _dbContext.AttractionDbM
            .AsNoTracking()
            .Include(a => a.CommentDbM)
                .ThenInclude(c => c.UsersDbM)
            .Take(number)
            .Select(a => new ReadAttractionSummaryDTO
            {
                AttractionId = a.AttractionId,
                Name = a.Name,
                Category = a.Category,
                Type = a.Type,
                City = a.City,
                Country = a.Country,
                Description = a.Description,
                HasComments = a.CommentDbM.Any(),
                Comments = a.CommentDbM.Select(c => new CommentDisplayDTO
                {
                    Text = c.Text,
                    UserName = c.UsersDbM.FullName
                }).ToList()
            })
            .ToListAsync();
        }

        else
        {
            return await _dbContext.AttractionDbM
           .AsNoTracking()
           .Where(i => !i.CommentDbM.Any())
           .Take(number)
           .Select(a => new ReadAttractionSummaryDTO
           {
               AttractionId = a.AttractionId,
               Name = a.Name,
               Category = a.Category,
               Type = a.Type,
               City = a.City,
               Country = a.Country,
               Description = a.Description,
               HasComments = false
           })
           .ToListAsync();

        }
    }
    public async Task<List<ReadAttractionsFilterDTO>> ReadAttractionFilter(bool seeded, string filter)
    {
        filter ??= "";

        var results = await _dbContext.AttractionDbM.AsNoTracking()
                .Where(i => (i.Seeded == seeded) && (
                    i.City.ToLower().Contains(filter) ||
                    i.Country.ToLower().Contains(filter) ||
                    i.Description.ToLower().Contains(filter) ||
                    i.Category.ToString().ToLower().Contains(filter) ||
                    i.Type.ToString().ToLower().Contains(filter)
        ))
        .Select(a => new ReadAttractionsFilterDTO
        {
            AttractionId = a.AttractionId,
            Name = a.Name,
            Category = a.Category,
            Type = a.Type,
            City = a.City,
            Country = a.Country,
            Description = a.Description
        })
        .ToListAsync();
        if (!results.Any())
        {
            throw new Exception($"The database does not contain anything with:{filter}");
        }
        return results;
    }

    public AttractionDbRepos(MainDbContext context)
    {
        _dbContext = context;
    }
}