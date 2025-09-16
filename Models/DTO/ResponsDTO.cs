using Microsoft.Identity.Client;

namespace Models.DTO;

public class ResponsDTO<T>
{
    public string ConnectionString { get; init; }
    public List<T> PageItems { get; init; }

    public int DbItemsCount { get; init; }


}

public class ResponsDTOItem<T>
{
    public T Item { get; init; }
}