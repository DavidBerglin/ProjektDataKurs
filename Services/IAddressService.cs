using Models;
using Models.DTO;

namespace Services;

public interface IAddressService
{
   public Task<List<AddressDTO>> ReadAddressAsync(int number);
}