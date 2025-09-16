using System.Net.Sockets;
using System.Security.Cryptography.Xml;
using Models;

namespace Models;

public interface IUsers
{
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public IAddress Address { get; set; }

}