using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Seido.Utilities.SeedGenerator;
using DbModels;
using DbContext;
using Configuration;
using Microsoft.VisualBasic;
using Models;

namespace DbRepos;

public class AdminDbRepos
{
    private readonly ILogger<AdminDbRepos> _logger;
    private Encryptions _encryptions;
    private readonly MainDbContext _dbContext;

    public async Task SeedAsync(int number)
    {
        var seeder = new SeedGenerator();

        //Skapa grundläggande entiteter med seed-data
        var address = seeder.ItemsToList<AddressDbM>(number);       
        var users = seeder.ItemsToList<UsersDbM>(number);           
        var attractions = seeder.ItemsToList<AttractionDbM>(number); 

        //Koppla Users till Addresses (Many-to-One, optional)
        foreach (var user in users)
        {
            var userAddress = seeder.Bool ? seeder.FromList(address) : null;
            user.AddressDbM = userAddress;                    
            user.AddressId = userAddress?.AddressId;         
        }

        // Koppla Attractions till Addresses (One-to-One, required)
        foreach (var attraction in attractions)
        {
            var AttractionAddress = seeder.FromList(address);
            attraction.AddressDbM = AttractionAddress;         
            attraction.AddressId = AttractionAddress.AddressId; 
            
            // Uppdatera attraction's city/country från address för konsistens
            attraction.City = AttractionAddress.City;
            attraction.Country = AttractionAddress.Country;
        }
        
        // Spara alla grundläggande entiteter till databasen
        await _dbContext.AddressDbM.AddRangeAsync(address);
        await _dbContext.AttractionDbM.AddRangeAsync(attractions);
        await _dbContext.UsersDbM.AddRangeAsync(users);
        await _dbContext.SaveChangesAsync(); 

        // Ladda om entiteter från databasen för att få korrekta IDs
        // Detta säkerställer att vi använder riktiga database IDs, inte seed IDs
        var savedAttractions = await _dbContext.AttractionDbM.ToListAsync();
        var savedUsers = await _dbContext.UsersDbM.ToListAsync();

        // Skapa Comments med kopplingar till befintliga Users och Attractions
        var comment = new List<CommentDbM>();
        foreach (var c in savedAttractions)
        {
            // Varje attraction får 0-20 kommentarer (slumpmässigt)
            int n = seeder.Next(0, 21);
            for (int i = 0; i < n; i++)
            {
                // Välj en slumpmässig user som kommenterar
                var u = seeder.FromList(savedUsers);
                
                comment.Add(new CommentDbM
                {
                    Seeded = true,
                    CommentId = Guid.NewGuid(),
                    Text = seeder.Quote.Quote,              // Slumpmässigt citat som kommentar
                    UserId = u.UserId,                      // FK till user (Many-to-One)
                    AttractionId = c.AttractionId           // FK till attraction (Many-to-One)
                });
            }
        }

        // STEG 7: Spara alla kommentarer
        await _dbContext.CommentDbM.AddRangeAsync(comment);
        await _dbContext.SaveChangesAsync();
    }

    public AdminDbRepos(ILogger<AdminDbRepos> logger, Encryptions encryptions, MainDbContext context)
    {
        _logger = logger;
        _encryptions = encryptions;
        _dbContext = context;
    }
}
