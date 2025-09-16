using Models;

namespace Services;

public interface IAddressService
{
   public Task<List<IAddress>> ReadAddressAsync(int number);
}