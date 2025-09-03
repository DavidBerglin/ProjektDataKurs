using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Data;

using Seido.Utilities.SeedGenerator;
using DbModels;
using DbContext;
using Configuration;
using models;
using Microsoft.VisualBasic;

namespace DbRepos;

public class AdminDbRepos
{
    private const string _seedSource = "./app-seeds.json";
    private readonly ILogger<AdminDbRepos> _logger;
    private Encryptions _encryptions;
    private readonly MainDbContext _dbContext;

    public async Task SeedAsync()
    {
        //Create a seeder
        var fn = Path.GetFullPath(_seedSource);
        var seeder = new SeedGenerator(fn);

        _dbContext.Quotes.RemoveRange(_dbContext.Quotes);
        _dbContext.CreditCardDbM.RemoveRange(_dbContext.CreditCardDbM);

        var creditcards = seeder.ItemsToList<CreditCardDbM>(1000);
        _dbContext.CreditCardDbM.AddRange(creditcards);

        //Seeding new quotes into the database
        var quotes = seeder.AllQuotes.Select(q => new QuoteDbM(q)).ToList();
        _dbContext.Quotes.AddRange(quotes);

        //Save changes to the database
        await _dbContext.SaveChangesAsync();
    }
    public async Task RemoveCreditCard(CancellationToken ct = default)
    {
        await _dbContext.CreditCardDbM.ExecuteDeleteAsync(ct);
    }

    public AdminDbRepos(ILogger<AdminDbRepos> logger, Encryptions encryptions, MainDbContext context)
    {
        _logger = logger;
        _encryptions = encryptions;
        _dbContext = context;
    }
}
