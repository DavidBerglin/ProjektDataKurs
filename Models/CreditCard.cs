using Models;
namespace models;
using Seido.Utilities.SeedGenerator;

public class CreditCard : ICreditCard, ISeed<CreditCard>
{
    public virtual Guid CreditCardId { get; set; }

    public CardIssuer Issuer { get; set; }
    public string CardNumber { get; set; }
    public string ExpiryMonth { get; set; }
    public string ExpiryYear { get; set; }

    public string CardHolderName { get; set; }

    public string EncryptedToken { get; set; } //AES encrypted version of the cc

    public bool Seeded { get; set; } = false;

    public CreditCard Seed(SeedGenerator seeder)
    {
        Seeded = true;
        CreditCardId = Guid.NewGuid();

        Issuer = seeder.FromEnum<CardIssuer>();

        CardNumber = $"{seeder.Next(2222, 9999)}-{seeder.Next(2222, 9999)}-{seeder.Next(2222, 9999)}-{seeder.Next(2222, 9999)}";
        ExpiryYear = $"{seeder.Next(25, 32)}";
        ExpiryMonth = $"{seeder.Next(01, 13):D2}";


        CardHolderName = seeder.FullName;
        return this;
    }
}
