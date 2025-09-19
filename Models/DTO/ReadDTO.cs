using Models.Enums;

namespace Models.DTO;

public class ReadAttractionDisplayDTO
{
    public string Name { get; set; }
    public AttractionCategory Category { get; set; }
    public string Description { get; set; }
    public List<CommentDisplayDTO> Comments { get; set; } = new List<CommentDisplayDTO>();
}

public class CommentDisplayDTO
{
    public string Text { get; set; }
    public string UserName { get; set; } 
}