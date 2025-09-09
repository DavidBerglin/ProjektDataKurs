using Models;
using Seido.Utilities.SeedGenerator;

namespace models;

public abstract class Address : IAddress, IEquatable<Address>, ISeed<Address>
{
    public virtual Guid AddressId { get; set; }
    public virtual string StreetAddress { get; set; }
    public virtual string City { get; set; }
    public virtual string Country { get; set; }
    public virtual int ZipCode { get; set; }

    public Attraction Attraction { get; set; }
    public virtual List<IUsers> Users { get; set; } = null;

    public bool Seeded { get; set; } = false;

    public bool Equals(Address other) => (other != null) && (this.StreetAddress, this.ZipCode, this.City) == (other.StreetAddress, other.ZipCode, other.City);
    public override int GetHashCode() => (StreetAddress, ZipCode, City).GetHashCode();

    public Address() {}
    public Address(Address OG)
    {
        this.AddressId = OG.AddressId;
        this.StreetAddress = OG.StreetAddress;
        this.City = OG.City;
        this.Country = OG.Country;
        this.ZipCode = OG.ZipCode;
    }
    public virtual Address Seed(SeedGenerator seeder)
    {
        AddressId = Guid.NewGuid();
        Country = seeder.Country;
        StreetAddress = seeder.StreetAddress(Country);
        City = seeder.City(Country);
        ZipCode = seeder.ZipCode;

        return this;
    }
}