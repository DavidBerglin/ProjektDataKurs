using Seido.Utilities.SeedGenerator;
namespace Models;

public class Users : IUsers, ISeed<Users>
{
    public virtual Guid UserId { get; set; }
    public virtual string FullName { get; set; }
    public virtual string Email { get; set; }
    public virtual IAddress Address { get; set; }
    public virtual List<IComments> Comments { get; set; }
    public bool Seeded { get; set; } = false;

    public Users() { }

    public Users(Users OG)
    {
        this.UserId = OG.UserId;
        this.FullName = OG.FullName;
        this.Email = OG.Email;
        this.Address = OG.Address;
        this.Comments = OG.Comments;
    }
    public virtual Users Seed(SeedGenerator seeder)
    {
        UserId = Guid.NewGuid();
        FullName = seeder.FullName;
        Email = seeder.Email(FullName);
        return this;

    }
}