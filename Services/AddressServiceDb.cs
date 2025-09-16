using Models;
using DbRepos;

namespace Services
{
    public class AddressServiceDb : IAddressService
    {
        private readonly AddressDbRepos _repo = null;

        public Task<List<IAddress>> ReadAddressAsync(int number)
        {
            return _repo.ReadAddressAsync(number);
        }
        public AddressServiceDb(AddressDbRepos repo)
        {
            _repo = repo;
        }
    }
}