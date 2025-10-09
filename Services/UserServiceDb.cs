using DbRepos;
using Models.DTO;

namespace Services
{
    public class UserServiceDb : IUserService
    {
        private readonly UserDbRepos _user = null;
        public Task<List<ReadUsersCommentsDTO>> ReadUsersAsync(int number) => _user.ReadUsersAsync(number);
        

        public UserServiceDb(UserDbRepos user)
        {
            _user = user;
        }
    }
}