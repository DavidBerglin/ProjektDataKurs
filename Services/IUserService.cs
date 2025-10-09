using Models.DTO;
namespace Services;

public interface IUserService
{
    public Task<List<ReadUsersCommentsDTO>> ReadUsersAsync(int number);
}