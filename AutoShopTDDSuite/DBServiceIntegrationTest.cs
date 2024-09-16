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

public class DBServiceIntegrationTest : IClassFixture<WebAppFactory>
{
    public WebAppFactory _webAppFactory { get; set; }
    public DBServiceIntegrationTest(WebAppFactory factory)
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
        //DBService dbService2 = TestSetupGetWebService();
        var custDetails = new EmailDTO
        {
            Name = "TesterMcTesterSon",
            Body = new Dictionary<string, string>
            {
                { "Name", "bob2" },
                { "Phone", "556565656" },
                { "Email", "testermctesterson@snow.edu" },
                { "License Plate", "1234567G"},
                { "Make", "Honda" },
                { "Model", "Thunderbird"},
                { "Year", "2025" },
                { "Odometer", "12345" },
                {"Comments", "It no work" }
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
}