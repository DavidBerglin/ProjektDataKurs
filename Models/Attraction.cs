namespace Models;
using Models;
using Seido.Utilities.SeedGenerator;

public class Attraction : IAttraction, ISeed<Attraction>
{
    public string Name { get; set; }

    public bool Seeded { get; set; } = false;

    public Attraction Seed(SeedGenerator seeder)
    {
        Name = seeder.FullName;
        return this;
    }
}
//