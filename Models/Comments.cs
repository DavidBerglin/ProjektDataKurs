using Models;
using Seido.Utilities.SeedGenerator;
namespace Models;

public class Comment : IComments, ISeed<Comment>
{
    public virtual Guid CommentId { get; set; }
    public virtual string Text { get; set; }
    public virtual IUsers User { get; set; }
    public virtual IAttraction Attraction { get; set; }

    public bool Seeded { get; set; } = false;

    public Comment() { }
    public Comment(Comment OG)
    {
        this.CommentId = OG.CommentId;
        this.Text = OG.Text;
    }
    public virtual Comment Seed(SeedGenerator seeder)
    {
        CommentId = Guid.NewGuid();
        Text = seeder.Quote.Quote;
        return this;
    }

}