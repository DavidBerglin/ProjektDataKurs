using Models.Enums;
namespace Models;

public interface IAttraction
{

    public Guid AttractionId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public AttractionType Type { get; set; }
    public AttractionCategory Category { get; set; }
   
    public IAddress Address { get; set; }
    public List<IComments> Comments { get; set; }
}