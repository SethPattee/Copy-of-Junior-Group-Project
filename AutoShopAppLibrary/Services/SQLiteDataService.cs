/*using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoShopAppLibrary.Data;

namespace AutoShopAppLibrary.Services;

public class SQLiteDataService : IDataService
{
    readonly DatabaseInitialize db = new DatabaseInitialize(); //it uses default name from class
    public SQLiteDataService()
    {
        db = new DatabaseInitialize();
        Startup();
    }

    public SQLiteDataService(string dbName)
    {
        db = new DatabaseInitialize(dbName);
        Startup();
    }

    public async Task Startup()
    {
        await db.InitializeLocalDatabase();
    }

    public async Task<Customer> AddCustomerAsync(Customer customer)
    {
        var dbConnection = db.GetConnection();
        await dbConnection.InsertAsync(customer);
        return customer;
    }
    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        var customers = await db.GetConnection().Table<Customer>().ToListAsync();
        return customers;
    }
    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
        var customer = await db.GetConnection().Table<Customer>().Where(
            e => e.Id == id).FirstOrDefaultAsync();
        return customer;
    }

    public async Task<Customer> GetCustomerByEmailAsync(string email)
    {
        var customer = await db.GetConnection().Table<Customer>().Where(
            e => e.Email == email).FirstOrDefaultAsync();
        return customer;
    }

    public async Task<int> UpdateCustomerAsync(Customer customer)
    {
        var updatedCutomer = await db.GetConnection().UpdateAsync(customer);
        return updatedCutomer;
    }

    public async Task<int> DeleteCustomerAsync(Customer customer)
    {
        int id = customer.Id;
        await db.GetConnection().DeleteAsync(customer);
        return id;
    }
    //TODO: fix

    public async Task<int> AddFormToDB(EmailDTO dto)
    {
        var customer = new Customer()
        {
            Name = dto.Body["Name"],
            Phone = dto.Body["Phone"],
            Email = dto.Body["Email"],
        };
        var car = new Car()
        {
            Make = dto.Body["Make"],
            Model = dto.Body["Model"],
            //Odometer = (double)dto.Body["Odometer"],
            Year = dto.Body["Year"]
        };

        return 0;
    }

    public async Task<bool> CustomerExists(string email)
    {
        var emails = await db.GetConnection().Table<Customer>().Where(e => e.Email == email).FirstOrDefaultAsync();
        if (emails.Equals(null)) { return false; };
        return true;
    }
}*/