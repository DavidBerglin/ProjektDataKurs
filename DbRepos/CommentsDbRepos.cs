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

namespace DbRepos;

public class CommentDbRepos
{
    private readonly MainDbContext _dbContext;

    public async Task<List<IComments>> ReadCommentsAsync(int number)
    {
      return await _dbContext.CommentDbM
        .AsNoTracking()
        .Include(c => c.usersDbM)          
        .Include(c => c.AttractionDbM)    
        .Take(number)
        .Cast<IComments>()
        .ToListAsync();
    }
    public CommentDbRepos(MainDbContext context)
    {
        _dbContext = context;
    }
}
  
