using Models;
using DbRepos;

namespace Services
{
    public class UserServiceDb : IUserService
    {
        private readonly UserDbRepos _user = null;
        public Task GetUsersAsync()
        {
            return _user.GetUsersAsync();
        }

        public UserServiceDb(UserDbRepos user)
        {
            _user = user;
        }
    }
}