using System.Security.Cryptography.Xml;

namespace models;

public interface IUsers
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }

    
}