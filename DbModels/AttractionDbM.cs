using Microsoft.Identity.Client;
using Models;
using Seido.Utilities.SeedGenerator;
using System.ComponentModel.DataAnnotations;
namespace DbModels;

public class AttractionDbM : Attraction, ISeed<AttractionDbM> , IEquatable<AttractionDbM>
{
    [Key]
    public override Guid? AttractionId { get; set; }

    public bool Equals(AttractionDbM other) => (other != null) ? AttractionId == other.AttractionId : false;
    public override int GetHashCode() => AttractionId.GetHashCode();
   
    public AttractionDbM() {}
    public new AttractionDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }

}