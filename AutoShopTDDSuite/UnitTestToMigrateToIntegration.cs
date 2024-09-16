using System.Configuration;
using System.Configuration.Internal;

using AutoShopAppLibrary;
using AutoShopAppLibrary.Data;
using AutoShopAppLibrary.Services;

using FluentAssertions;

using Microsoft.Extensions.Configuration;
namespace AutoShopTDDSuite;

public class UnitTestToMigrateToIntegration
{
    //private readonly string connectionString;
    //private readonly string dumpFilePath = "./20240401.06.23.12.sql";

    [Fact]
    public void Test1()
    {
        Assert.True(true);
    }

    //CREATE
    //[Fact]
    //public async Task Test_CanAddCustomerToDB_returnsSameCustomer_isTrue()
    //{




    //    //DataService testDataService = new DataService(connectionString);

    //    // Arrange
    //    string connectionString = "connectionString";
    //    DataService testDataService = new DataService(connectionString);

    //    var testCustomer = new Customer
    //    {
    //        // Id will be assigned by the database, so leave it as default (0 or null)
    //        Email = "1234567891234567@gmail.com",
    //        Name = "testCustomer1",
    //        Phone = "1234567890"
    //    };

    //    // Act
    //    var returnedCustomer = await testDataService.AddCustomerAsync(testCustomer);

    //    // Assert

    //    Assert.Equal(returnedCustomer, testCustomer);
    //}


    ////READ
    //[Fact]
    //public async Task Test_CanAddCustomerToDB_ReadsFromDataBase()
    //{
    //    //Arrange
    //    DatabaseInitialize dbtestservice = new DatabaseInitialize("testDB2");
    //    DataService testDataService = new DataService("testDB2");
    //    await dbtestservice.RestartDatabase();
    //    var testCustomer = new Customer
    //    {
    //        Email = "1234567891234567@gmail.com",
    //        Name = "testCustomer1",
    //        Phone = "1234567890"
    //    };
    //    await testDataService.AddCustomerAsync(testCustomer);
    //    //Act
    //    var readCustomer = await testDataService.GetCustomerByEmailAsync("1234567891234567@gmail.com");
    //    string email = readCustomer.Email;

    //    string testCustomeremail = testCustomer.Email;
    //    //Assert
    //    await dbtestservice.RestartDatabase();
    //    Assert.Equal(testCustomeremail, email);
    //}

    ////Update
    //[Fact]
    //public async Task Test_CanUpdateCustomer()
    //{
    //    //Arrange
    //    DatabaseInitialize dbtestservice = new DatabaseInitialize("testDB2");
    //    DataService testDataService = new DataService("testDB2");
    //    await dbtestservice.RestartDatabase();
    //    var testCustomer = new Customer
    //    {
    //        Email = "1234567891234567@gmail.com",
    //        Name = "testCustomer1",
    //        Phone = "1234567890"
    //    };

    //    var updated = new Customer
    //    {
    //        Email = "1234567891234567@gmail.com",
    //        Name = "testCustomer1",
    //        Phone = "notAPhone"
    //    };
    //    testCustomer = updated;
    //    await testDataService.AddCustomerAsync(testCustomer);
    //    //Act
    //    await testDataService.UpdateCustomerAsync(updated);
    //    var readCustomer = await testDataService.GetCustomerByEmailAsync("1234567891234567@gmail.com");

    //    //Assert phone has changed
    //    Assert.Equal(readCustomer.Phone, updated.Phone);
    //}

    //[Fact]
    //public async Task Test_CanDeleteCustomerFromDB()
    //{
    //    //Arrange
    //    DataService testDataService = new DataService("testDB1");
    //    var testCustomer = new Customer
    //    {
    //        Id = 1,
    //        Email = "1234567891234567@gmail.com",
    //        Name = "testCustomer1",
    //        Phone = "1234567890"
    //    };
    //    //Act
    //    var returnedCustomer = await testDataService.AddCustomerAsync(testCustomer);
    //    await testDataService.DeleteCustomerAsync(returnedCustomer);

    //    //Assert
    //    var retrieved = testDataService.GetAllCustomersAsync().Result.Where(x => x.Id == returnedCustomer.Id);
    //    Assert.Empty(retrieved);
    //}

    ////TESTS to see if we can combine customers properly upon form submission
    //public DataService CompareCustomerTestSetup()
    //{
    //    return new DataService("testDB_" + DateTime.Now.Second.ToString());
    //}

    //public void CompareCustomerTestCleanup(DataService service)
    //{

    //}

    //[Fact]
    //public void CanCallCompareCustomerFunction()
    //{
    //    DataService testDataService = CompareCustomerTestSetup();
    //    testDataService.CustomerExists("example@mail.com");
    //}

    //[Fact]
    //public void CustomerExists_returnsFalse_whenCustomerDoesNotExist()
    //{
    //    DataService testDataService = CompareCustomerTestSetup();
    //    Assert.False(testDataService.CustomerExists("example@mail.com").Result);
    //}

    ////[Fact]
    ////public void CustomerDoesNotExist_returnsTrue_whenCustomerExists()
    ////{
    ////    DataService testDataService = CompareCustomerTestSetup();
    ////    Assert.True(testDataService.CustomerExists("example@mail.com").Result);
    ////}

    //[Fact]
    //public async Task CanRetrieveAllCustomers()
    //{
    //    DataService testDataService = CompareCustomerTestSetup();
    //    await testDataService.AddCustomerAsync( new Customer {
    //        Name = "test",
    //        Email = "test@mail.com",
    //        Phone = "123 456"
    //    });
    //}
}