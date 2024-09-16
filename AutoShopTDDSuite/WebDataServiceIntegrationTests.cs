using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using AutoShopAppLibrary.Components;
using AutoShopAppLibrary.Data;
using AutoShopAppLibrary.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.VisualStudio.TestPlatform.TestHost;
using Renci.SshNet;

using Xunit.Sdk;

namespace AutoShopTDDSuite;

public class WebDataServiceIntegrationTests : IClassFixture<WebAppFactory>
{
    public WebAppFactory _webAppFactory { get; set; }
    public WebDataServiceIntegrationTests(WebAppFactory factory)
    {
        factory.CreateDefaultClient();
        _webAppFactory = factory;
    }

    public IDataService TestSetupGetWebService()
    {
        using var scope = _webAppFactory.Services.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        var webDataService = serviceProvider.GetRequiredService<IDataService>();
        return webDataService;
    }

    [Fact]
    public void test1()
    {
        //this.CreateDefaultClient();
        Assert.True(true);
    }
    [Fact]
    public void TestWebAppFactory()
    {
        var webDataService = TestSetupGetWebService();
    }

    [Fact]
    public async Task TestAddValidCustomer()
    {
        //Arrange
        var webDataService = TestSetupGetWebService();

        var testCustomer = new AutoShopAppLibrary.Data.Customer
        {
            // Id will be assigned by the database, so leave it as default (0 or null)
            Email = "1234567891234567@gmail.com",
            Name = "testCustomer1",
            Phone = "1234567890"
        };

        // Act
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var returnedCustomer = await webDataService.AddCustomerAsync(testCustomer);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        // Assert
        Assert.Equal(returnedCustomer, testCustomer);
    }

    [Fact]
    public async Task TestAddNullCustomer()
    {
        var webDataService = TestSetupGetWebService();

        //act
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var returnedCustomer = await webDataService.AddCustomerAsync(null);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        //Assert
        Assert.Null(returnedCustomer);

    }

    [Fact]
    public async Task TestGetRealCustomerByEmail()
    {
        //Arrange
        var webDataService = TestSetupGetWebService();
        //act
        var cust = new Customer
        {
            Email = "email@example.com",
            Name = "test customer"
        };
        if (webDataService is not null)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            await webDataService.AddCustomerAsync(cust);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var returnedCustomer = await webDataService.GetCustomerByEmailAsync("email@example.com");
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            //Assert
            Assert.Equal(cust.Email, returnedCustomer.Email);
        }
        else
        {
            Assert.Fail(); //if our web data service was null, we definitely couldn't test correctly.
        }



    }

    [Fact]
    public async void TestGetCustomerByEmail_returnsNullOnBadEmail()
    {
        //Arrange
        var webDataService = TestSetupGetWebService();

        //act
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var returnedCustomer = await webDataService.GetCustomerByEmailAsync("bademail");
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        //Assert
        Assert.Null(returnedCustomer);

    }

    [Fact]
    public async Task TestCanSubmitNotExistingCustomerForm()
    {
        //Arrange
        var webDataService = TestSetupGetWebService();

        //act
        //var returnedItem = await webDataService.AddFormToDB(new )
    }
}

//[Fact]
//public async Task Test_CanAddCustomerToDB_returnsSameCustomer_isTrue()
//{
//    WebAppFactory _factory;
//    _factory = new WebAppFactory();

//    var webService = new WebDataService(_factory);

//    var testCustomer = new Customer
//    {
//        // Id will be assigned by the database, so leave it as default (0 or null)
//        Email = "1234567891234567@gmail.com",
//        Name = "testCustomer1",
//        Phone = "1234567890"
//    };

//    // Act
//    var returnedCustomer = await WebDataService.instance.AddCustomerAsync(testCustomer);

//    // Assert
//    Assert.Equal(returnedCustomer, testCustomer);
//}
//}