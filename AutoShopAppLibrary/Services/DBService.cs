

using AutoShopAppLibrary.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AutoShopAppLibrary.Services;

public class DBService : IDBService
{
    private readonly PostgresContext _context;
    private readonly ILogger<DBService> _logger;

    public DBService(PostgresContext context, ILogger<DBService> loggger)
    {
        _logger = loggger;
        _context = context;
    }

    public async Task<bool> UpdateCustomerDetailsInDatabase(EmailDTO customerDetails)
    {
        _logger.LogInformation("Request received: update an user's information.\nStarting update procedure at " + DateTime.Now.ToString());

        try
        {
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == customerDetails.Body["Email"]);
            if (existingCustomer == null)
            {
                _logger.LogCritical("Update aborted at " + DateTime.Now + "\n-> User information could not be found!");
                return false;
            }

            _logger.LogInformation("User information successfully retrieved. Applying update at " + DateTime.Now.ToString());
            existingCustomer.Name = customerDetails.Name;
            existingCustomer.Phone = customerDetails.Body["Phone"];
            _context.Customers.Update(existingCustomer);

            _logger.LogInformation("User information successfully updated. Saving changes at " + DateTime.Now.ToString());
            await _context.SaveChangesAsync();
            _logger.LogInformation("Changes successfully saved. Process complete at " + DateTime.Now.ToString());

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("An unexpected error has occurred at " + DateTime.Now.ToString() + " -> " + ex.Message);
            return false;
        }
    }


    public async Task<bool> StoreCustomerDetailsInDatabase(EmailDTO customerDetails)
    {
        try
        {
            _logger.LogInformation("Parsing form info...");


            //Work with customer records
            _logger.LogInformation("Starting Customer information recording");
            Customer customer = new()
            {
                Name = customerDetails.Body["Name"],
                Phone = customerDetails.Body["Phone"],
                Email = customerDetails.Body["Email"]
            };
            await StoreCustomer(customer);
            _logger.LogInformation("Customer information successfully recorded.");


            //Work with car records
            _logger.LogInformation("Starting Car information recording");
            Car car = new()
            {
                LicensePlate = customerDetails.Body["License Plate"],
                Make = customerDetails.Body["Make"],
                Model = customerDetails.Body["Model"],
                Year = customerDetails.Body["Year"],
            };
            int CustId = await StoreCar(customer, car);
            _logger.LogInformation("Car information successfully recorded.");


            //Work with work order records
            _logger.LogInformation("Starting work order information recording");
            double odoVar;
            if (!Double.TryParse(customerDetails.Body["Odometer"], out odoVar))
            {
                var e = new InvalidCastException("The provided odometer could not be converted to a number!");
                _logger.LogError("Exception break at " + DateTime.Now.ToString() + " -> " + e.Message);
                throw e;
            }
            Workorder workorder = new Workorder()
            {
                Odometer = odoVar,
                Concerns = customerDetails.Body["Comments"]
            };
            await StoreWorkOrder(CustId, workorder);
            _logger.LogInformation("Work order information successfully recorded.");
        }
        catch (InvalidCastException ex)
        {
            throw new Exception("odometer");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return true;
    }
    ////////////////////////////////////////////////////////////////////////
    private async Task StoreCustomer(Customer customer)
    {
        var responce = await _context.Customers.Where(c => c.Email == customer.Email).FirstOrDefaultAsync();
        if (responce == null)
        {
            await PlaceCustomerInDB(customer);
        }
    }

    public async Task PlaceCustomerInDB(Customer customer)
    {
        try
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogInformation("placing customer in DB threw error: " + ex.Message);
        }

    }
    ////////////////////////////////////////////////////////////////////////////////////
    private async Task<int> StoreCar(Customer customer, Car car)
    {
        car.CustId = (await _context.Customers.Where(c => c.Email == customer.Email).FirstOrDefaultAsync()).Id;
        var x = await _context.Cars.Where(c => c.CustId == car.CustId
                                            && c.LicensePlate == car.LicensePlate).FirstOrDefaultAsync();

        if (x != null)
        {
            return car.CustId;
        }

        await _context.Cars.AddAsync(car);
        await _context.SaveChangesAsync();
        return car.CustId;
    }

    private async Task StoreWorkOrder(int CustId, Workorder workorder)
    {
        var car = await _context.Cars.Where(c => c.CustId == CustId).FirstOrDefaultAsync();
        workorder.CarId = car.Id;
        workorder.CustId = CustId;
        workorder.Datesubmitted = DateTime.Now.Date.ToString("MM/dd/yyyy");

        await _context.Workorders.AddAsync(workorder);
        await _context.SaveChangesAsync();
    }

    private async Task<int> GetNumberofCustomers()
    {
        int numberofcustomers;
        numberofcustomers = await _context.Customers.CountAsync();
        return numberofcustomers;
    }

    private async Task<int> GetNumberofWorkorders()
    {
        int workorders;
        workorders = await _context.Workorders.CountAsync();
        return workorders;
    }

    private async Task<int> GetNumberofCars()
    {
        int cars;
        cars = await _context.Workorders.CountAsync();
        return cars;
    }


}