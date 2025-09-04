using DbModels;
using Models;
using Seido.Utilities.SeedGenerator;
using System.ComponentModel.DataAnnotations;
namespace DbModels;

public class AttractionDbM : Attraction
{
    [Key]
    public new string Name { get; set; }

    public AddressDbM Address { get; set; } = null;

    [NotMapped]
    public new IAddress Address { get => AddressDbM; set => new NotImplementedException(); }
}