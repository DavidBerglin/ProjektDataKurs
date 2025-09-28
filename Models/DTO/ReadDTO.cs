using Microsoft.Identity.Client;
using Models.Enums;

namespace Models.DTO;
// DTO till kommentarer
public class CommentDisplayDTO
{
    public string Text { get; set; }
    public string UserName { get; set; }
}
// DTO till attraktioner
public class ReadAttractionSummaryDTO
{
    public Guid AttractionId { get; set; }
    public string Name { get; set; }
    public AttractionCategory Category { get; set; }

    public AttractionType Type { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Description { get; set; }
    public bool HasComments { get; set; }
    public List<CommentDisplayDTO> Comments { get; set; } = new List<CommentDisplayDTO>();

}
public class ReadAttractionsFilterDTO
{
    public Guid AttractionId { get; set; }
    public string Name { get; set; }
    public AttractionCategory Category { get; set; }

    public AttractionType Type { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Description { get; set; }
}
// DTO till användare och dennes kommentarer
public class ReadUsersCommentsDTO
{
    public Guid UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public List<UserCommentDTO> Comments { get; set; } = new List<UserCommentDTO>();

}

// DTO kommentarer med relationer
public class UserCommentDTO
{
    public Guid CommentId { get; set; }
    public string Text { get; set; }

    public Guid AttractionId { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}

// DTO Addresser
public class AddressDTO
{
    public Guid AddressId { get; set; }
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}