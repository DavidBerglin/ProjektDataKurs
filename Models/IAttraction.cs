using Models.Enums;
namespace Models;

public interface IAttraction
{

    public Guid? AttractionId { get; set; }

    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public AttractionType Type { get; set; }
    public AttractionCategory Category { get; set; }
}