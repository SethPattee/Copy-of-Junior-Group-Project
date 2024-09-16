using AutoShopAppLibrary.Data;

namespace AutoShopAppLibrary.Services;

public interface IDBService
{

    Task<bool> StoreCustomerDetailsInDatabase(EmailDTO customerDetails);
    Task<bool> UpdateCustomerDetailsInDatabase(EmailDTO customerDetails);
    public Task PlaceCustomerInDB(Customer customer);
}