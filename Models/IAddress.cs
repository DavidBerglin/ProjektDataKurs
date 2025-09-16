using Microsoft.AspNetCore.Identity;
using Models;
namespace Models;

public interface IAddress
{
    public Guid AddressId { get; set; }
    public string StreetAddress { get; set; }
    public int ZipCode { get; set; }
    public string City { get; set; }

    public string Country { get; set; }

    public List<IUsers>Users { get; set;}
}