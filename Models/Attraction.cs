namespace Models;
using Models;
using Seido.Utilities.SeedGenerator;

public class Attraction : IAttraction
{
    public string Name { get; set; }
    public IAddress Address { get; set; }

    public bool Seeded { get; set; } = false;

}
//