using Models;

namespace Services;

public interface IUserService
{
    public Task<List<IUsers>> ReadUsersAsync(int number);
}