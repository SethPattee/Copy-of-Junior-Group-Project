using AutoShopAppLibrary.Data;

namespace AutoShopAppLibrary.Services
{
    public interface IDataService
    {
        Task<Customer>? AddCustomerAsync(Customer? customer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer>? GetCustomerByEmailAsync(string email);
        Task<int> UpdateCustomerAsync(Customer customer);
        Task<int> DeleteCustomerAsync(Customer customer);
        Task<int> AddFormToDB(EmailDTO dto);
        Task<int> GetNumberofWorkorders();
        Task<int> GetNumberofCars();
        Task<int> GetNumberofCustomers();
    }
}