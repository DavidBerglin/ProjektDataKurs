namespace Services;

public interface IUserService
{
    public Task GetUsersAsync(int number);
}