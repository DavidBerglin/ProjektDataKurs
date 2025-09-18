using Microsoft.Identity.Client;
using Models;
using Seido.Utilities.SeedGenerator;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Compression;
namespace DbModels;

public class AttractionDbM : Attraction, ISeed<AttractionDbM> , IEquatable<AttractionDbM>
{
    [Key]
    public override Guid AttractionId { get; set; }

    public override Guid AddressId { get; set; }


    [NotMapped]
    public override IAddress Address { get => AddressDbM; set => new NotImplementedException(); }
    public AddressDbM AddressDbM { get; set; } = null;

    [NotMapped]
    public override List<IComments> Comments { get => CommentDbM?.ToList<IComments>(); set => new NotImplementedException();}
    public List<CommentDbM> CommentDbM { get; set; } = null;


    public AttractionDbM() { }
    public new AttractionDbM Seed(SeedGenerator seeder)
    {
        base.Seed(seeder);
        return this;
    }
    public bool Equals(AttractionDbM other) => (other != null) ? AttractionId == other.AttractionId : false;
    public override int GetHashCode() => AttractionId.GetHashCode();
}