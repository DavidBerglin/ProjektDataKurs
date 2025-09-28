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
using Models.DTO;


namespace DbRepos;

public class UserDbRepos
{
    private readonly MainDbContext _dbContext;

    public async Task<List<ReadUsersCommentsDTO>> ReadUsersAsync()
    {
        return await _dbContext.UsersDbM
        .AsNoTracking()
        .Include(a => a.CommentDbM)
        .ThenInclude(a => a.AttractionDbM)
        .Where(a => a.CommentDbM.Any())
        .Select(user => new ReadUsersCommentsDTO
       {
            UserId = user.UserId,
            FullName = user.FullName,
            Email = user.Email,
            Comments = user.CommentDbM.Select(comment => new UserCommentDTO
            {
                CommentId = comment.CommentId,
                Text = comment.Text,
                AttractionId = comment.AttractionDbM.AttractionId,
                Name = comment.AttractionDbM.Name,
                City = comment.AttractionDbM.City,
                Country = comment.AttractionDbM.Country
            }).ToList()
        })
        .ToListAsync();

    
        
    }
    public UserDbRepos(MainDbContext context)
    {
        _dbContext = context;
    }
}