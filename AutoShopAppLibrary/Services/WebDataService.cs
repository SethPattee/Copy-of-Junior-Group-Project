using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoShopAppLibrary.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AutoShopAppLibrary.Services;

public class WebDataService : IDataService
{
    private readonly IDbContextFactory<PostgresContext> factory;
    private readonly IConfiguration _config;

    public WebDataService(IDbContextFactory<PostgresContext> _factory, IConfiguration config)
    {
        this.factory = _factory;
        _config = config;
    }
    public async Task<Customer>? AddCustomerAsync(Customer? customer)
    {
        if (customer is null)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return null;
            //#pragma warning restore CS8603 // Possible null reference return.
        }
        var contextFact = factory.CreateDbContext();
        var addedCust = contextFact.Add(customer).Entity;
        await contextFact.SaveChangesAsync();
        return addedCust;
    }

    public async Task<Customer>? GetCustomerByEmailAsync(string email)
    {
        var contextFact = factory.CreateDbContext();
        var customer = await contextFact.Customers.Where(c => c.Email == email).FirstOrDefaultAsync();
        return customer;
    }

    public Task<int> AddFormToDB(EmailDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        throw new NotImplementedException();
    }



    public Task<Customer> GetCustomerByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetNumberofCustomers()
    {
        var contextFact = factory.CreateDbContext();
        int numberofcustomers;
        numberofcustomers = await contextFact.Customers.CountAsync();
        return numberofcustomers;
    }

    public async Task<int> GetNumberofWorkorders()
    {
        var contextFact = factory.CreateDbContext();
        int workorders;
        workorders = await contextFact.Workorders.CountAsync();
        return workorders;
    }

    public async Task<int> GetNumberofCars()
    {
        var contextFact = factory.CreateDbContext();
        int cars;
        cars = await contextFact.Cars.CountAsync();
        return cars;
    }
}