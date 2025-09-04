using Seido.Utilities.SeedGenerator;
namespace Models;

public class Address : IAddress, IEquatable<Address>
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    public Address(){ }

    public Address(Address org) {
        Street = org.Street;
        City = org.City;
        State = org.State;
        ZipCode = org.ZipCode;
    }

    public bool Equals(Address other) => (other != null) && ((this.StreetAddress, this.ZipCode, this.City, this.Country) ==
        (other.StreetAddress, other.ZipCode, other.City, other.Country));

    public override bool Equals(object obj) => Equals(obj as Address);
    public override int GetHashCode() => (StreetAddress, ZipCode, City, Country).GetHashCode();

}