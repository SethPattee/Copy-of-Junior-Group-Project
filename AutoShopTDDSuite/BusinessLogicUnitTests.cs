/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoShopAppLibrary.Data;
using AutoShopAppLibrary.Logic;
using AutoShopAppLibrary.Services;
using Moq;

using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Logging;

namespace AutoShopTDDSuite;

public class BusinessLogicUnitTests
{
   *//* [Fact]
    public void StoreCustomerDoesNotAddPreviouslyExistingCustomerToDatabase()
    {
        //Arrange
        var newCustomer = new Customer
        {
            Email = "goodEmail@example.com"
        };
        var mockDBService = new Mock<DBService>();
        mockDBService.Setup(db => db.PlaceCustomerInDB(newCustomer))
                         .Returns(true); // Assuming it returns a boolean indicating success

        //act
        //try to add previously existing cutomer to DB

        //Assert
        //add customer to db was not called
    }*//*

    [Fact]
    public async Task StoreCustomerDetailsInDatabase_Success()
    {            
        // Arrange
        var customerDetails = new EmailDTO
        {
            Name = "TesterMcTesterSon",
            Body = new Dictionary<string, string>
             {
                 { "Name", "bob2" },
                 { "Phone", "556565656" },
                 { "Email", "testermctesterson@snow.edu" },
                 { "License Plate", "12345"},
                 { "Make", "Honda" },
                 { "Model", "Thunderbird"},
                 { "Year", "2025" },
                 { "Odometer", "12345" },
                 {"Comments", "It no work" }
             }
        };

        var context = new Mock<PostgresContext>();
        var logger = new Mock<ILogger<DBService>>();

        //var dbServiceMock = new Mock<IDBService>();
       

        //dbServiceMock.Setup(m => m.StoreCustomerDetailsInDatabase(It.IsAny<AutoShopAppLibrary.Data.EmailDTO>()))
            //.Returns(Task.FromResult(true));
            //.Verifiable(); // Verifying that the method is called

        var dbService = new DBService(context.Object, logger.Object);

        // Act
        await dbService.PlaceCustomerInDB(new AutoShopAppLibrary.Data.Customer());

        // Assert
        //dbServiceMock.Verify(m => m.PlaceCustomerInDB(It.IsAny<AutoShopAppLibrary.Data.Customer>()), Times.Once);
        Assert.True(await dbService.StoreCustomerDetailsInDatabase(customerDetails));
    }
}

public class TestDBService : IDBService
{
    bool PlaceCustomerInDBWasCalled = false;
    public Task<bool> StoreCustomerDetailsInDatabase(EmailDTO customerDetails)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateCustomerDetailsInDatabase(EmailDTO customerDetails)
    {
        throw new NotImplementedException();
    }

    public async Task PlaceCustomerInDB(Customer customer)
    {
        PlaceCustomerInDBWasCalled = true;
        await Task.CompletedTask;
    }
}*/