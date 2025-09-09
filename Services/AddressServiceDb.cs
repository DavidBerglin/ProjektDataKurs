using Models;
using DbRepos;

namespace Services
{
    public class AddressServiceDb : IAddressService
    {
        private readonly AddressDbRepos _repo = null;

        public Task GetAddressAsync()
        {
            return _repo.GetAddressAsync();
        }
        public AddressServiceDb(AddressDbRepos repo)
        {
            _repo = repo;
        }
    }
}