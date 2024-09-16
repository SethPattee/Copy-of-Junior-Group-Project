using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoShopAppLibrary.Data;
using AutoShopAppLibrary.Services;

using AutoShopWeb.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutoShopTDDSuite;

public class PostgresServiceIntegrationTests : IClassFixture<WebAppFactory>
{
    public WebAppFactory _webAppFactory { get; set; }
    public PostgresServiceIntegrationTests(WebAppFactory factory)
    {
        factory.CreateDefaultClient();
        _webAppFactory = factory;
    }

    [Fact]
    public async void UpdateCustomerDetailsInDatabaseSuccessfullyUpdatesNormalData()
    {
        //Arrange
        using var scope = _webAppFactory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var dbService = serviceProvider.GetRequiredService<DBService>();
        DBService dbService2 = TestSetupGetWebService();
        var custDetails = new EmailDTO
        {
            Name = "Tester",
            Body = new Dictionary<string, string>
             {
                 { "Name", "bob2" },
                 { "Phone", "5566666666" },
                 { "Email", "j@gmail.com" },
                 { "License Plate", "12345"},
                 { "Make", "Honda" },
                 { "Model", "Thunder"},
                 { "Year", "2001" },
                 { "Odometer", "12345" },
                 {"Comments", "It" }
             }
        };
        var changedCustDetails = custDetails;
        changedCustDetails.Body["Name"] = "Jane";
        changedCustDetails.Body["Phone"] = "1234567890";

        //Act
        var storeresult = await dbService.StoreCustomerDetailsInDatabase(custDetails);
        if (!storeresult) { Assert.True(false); }

        var updateresult = await dbService.UpdateCustomerDetailsInDatabase(changedCustDetails);

        //Assert
        Assert.True(updateresult);
    }

    public DBService TestSetupGetWebService()
    {
        using var scope = _webAppFactory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var dbService = serviceProvider.GetRequiredService<DBService>();
        return dbService;
    }


    [Fact]
    public void test1()
    {
        //this.CreateDefaultClient();
        Assert.True(true);
    }
    [Fact]
    public void MakeSureNoBreak()
    {
        DBService dbService = TestSetupGetWebService();
        Assert.True(true);
    }
    [Fact]
    public async void StoreCustomerDetailsInDatabaseActuallyDoesItsJob()
    {
        //Arrange
        using var scope = _webAppFactory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var dbService = serviceProvider.GetRequiredService<DBService>();
        //DBService dbService = TestSetupGetWebService();
        var emailDto = new EmailDTO
        {
            Name = "TesterMcTesterSon",
            Body = new Dictionary<string, string>
             {
                 { "Name", "bob" },
                 { "Phone", "5555555555" },
                 { "Email", "tester@snow.edu" },
                 { "License Plate", "1234Ged"},
                 { "Make", "Honda" },
                 { "Model", "Thunderbird"},
                 { "Year", "2025" },
                 { "Odometer", "1234567838" },
                 {"Comments", "It no work" }
             }
        };
        //Act
        var result = await dbService.StoreCustomerDetailsInDatabase(emailDto);
        //Assert
        Assert.True(result);
    }


}