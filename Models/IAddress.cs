namespace Models;

public interface IAddress
{
    string Street { get; set; }
    string City { get; set; }
    string State { get; set; }
    string ZipCode { get; set; }

    public List<IAttraction> Attraction { get; set; }

}