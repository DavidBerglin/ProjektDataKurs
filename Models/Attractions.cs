
using Microsoft.Extensions.Logging;
using Seido.Utilities.SeedGenerator;
using Models.Enums;
namespace Models
{

    public class Attraction : ISeed<Attraction>
    {
        public Guid AttractionId { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public AttractionType Type { get; set; }

        public AttractionCategory Category { get; set; }

        public bool Seeded { get; set; } = false;
        public override string ToString() =>  $"Id: {AttractionId}. \nName: {Name}. \nType: {Type}. \nCategory: {Category.ToString().Replace("And", " and ")}, \nCity: {City}. \nCountry: {Country}. \nAddress: {Address} \n -------";


        public Attraction Seed(SeedGenerator rnd)
        {
            AttractionId = Guid.NewGuid();
            Country = rnd.Country;
            Type = rnd.FromEnum<AttractionType>();
            Address = rnd.StreetAddress(Country);
            City = rnd.City(Country);
            Category = Type.GetCategory();
            Name = $"{rnd.LastName}'s {Type} of {City}";
            return this;

        }
    }
}