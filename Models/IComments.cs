using Microsoft.Identity.Client;
using Models;
using Models.Enums;
namespace Models;


public interface IComments
{
    public Guid CommentId { get; set; }
    public string Text { get; set; }
    public IUsers User { get; set; }
    public IAttraction Attraction { get; set; }
}