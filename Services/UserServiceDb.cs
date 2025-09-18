using Models;
using DbRepos;

namespace Services
{
    public class UserServiceDb : IUserService
    {
        private readonly UserDbRepos _user = null;
        public Task<List<IUsers>> ReadUsersAsync(int number)
        {
            return _user.ReadUsersAsync(number);
        }

        public UserServiceDb(UserDbRepos user)
        {
            _user = user;
        }
    }
}