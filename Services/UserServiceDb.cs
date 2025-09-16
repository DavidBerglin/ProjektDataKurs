using Models;
using DbRepos;

namespace Services
{
    public class UserServiceDb : IUserService
    {
        private readonly UserDbRepos _user = null;
        public Task GetUsersAsync(int number)
        {
            return _user.GetUsersAsync(number);
        }

        public UserServiceDb(UserDbRepos user)
        {
            _user = user;
        }
    }
}