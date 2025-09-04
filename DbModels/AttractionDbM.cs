using DbModels;
using Models;
using Seido.Utilities.SeedGenerator;
using System.ComponentModel.DataAnnotations;
namespace DbModels;

public class AttractionDbM : Attraction , ISeed<AttractionDbM>
{
    [Key]
    public new string Name { get; set; }

    public new AttractionDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }
}