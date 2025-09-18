
using Microsoft.Extensions.Logging;
using Seido.Utilities.SeedGenerator;
using Models.Enums;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
namespace Models
{

    public class Attraction : IAttraction,  ISeed<Attraction>, IEquatable<Attraction>
    {
        public virtual Guid AttractionId { get; set; }
        public virtual string Name { get; set; }
        public virtual Guid AddressId { get; set; }
        public virtual IAddress Address { get; set; }
        public virtual string Description { get; set; }
        public virtual string City { get; set; }
        public virtual string Country { get; set; }
        public virtual AttractionType Type { get; set; }
        public virtual AttractionCategory Category { get; set; }
        public virtual List<IComments> Comments { get; set; }
        public bool Seeded { get; set; } = false;

        public bool Equals(Attraction other) => (other != null) ? (AttractionId) == (other.AttractionId) : false;
        public override bool Equals(object obj) => Equals(obj as Attraction);
        public override int GetHashCode() => AttractionId.GetHashCode();
       
       

        public virtual Attraction Seed(SeedGenerator rnd)
        {
            AttractionId = Guid.NewGuid();
            Description = rnd.FromString("Great place, Lovely stop, Beautiful spot, Great value");
            Type = rnd.FromEnum<AttractionType>();
            Category = Type.GetCategory();
            Name = $"{rnd.LastName}'s {Type} of {City}";
            return this;

        }

        
    }
}