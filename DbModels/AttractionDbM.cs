using DbModels;
using Models;
using Seido.Utilities.SeedGenerator;
using System.ComponentModel.DataAnnotations;
namespace DbModels;

public class AttractionDbM : Attraction
{
    [Key]

    public Guid Id { get; set; }   // tydlig PK

    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public string Street { get; set; }
    
    public int Zip { get; set; }

}