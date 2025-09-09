using Models;
using DbRepos;

namespace Services
{
    public class UserServiceDb : IUserService
    {
        private readonly UserServiceDb _user = null;
        public Task GetUsersAsync()
        {
            return _user.GetUsersAsync();
        }

        public UserServiceDb(UserServiceDb user)
        {
            _user = user;
        }
    }
}