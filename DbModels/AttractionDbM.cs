using DbModels;
using models;
using Seido.Utilities.SeedGenerator;
using System.ComponentModel.DataAnnotations;
namespace DbModels;

public class AttractionDbM : Attraction , ISeed<AttractionDbM>
{
    [Key]
    public override string Name { get; set; }

    public new AttractionDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }
}