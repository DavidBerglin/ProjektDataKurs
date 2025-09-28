using Models;
using DbRepos;
using Models.DTO;

namespace Services
{
    public class AddressServiceDb : IAddressService
    {
        private readonly AddressDbRepos _repo = null;

        public Task<List<AddressDTO>> ReadAddressAsync(int number) =>  _repo.ReadAddressAsync(number);
        
        public AddressServiceDb(AddressDbRepos repo)
        {
            _repo = repo;
        }
    }
}