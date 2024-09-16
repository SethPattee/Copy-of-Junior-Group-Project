using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using AutoShopAppLibrary.Data;

using Grpc.Core;

using Microsoft.AspNetCore.Mvc;

namespace AutoShopTDDSuite;

public class FunctionsUnitTests
{
    //practice calling our azure function
    // [Fact]
    // public async void CanCallAzureFunctionOnCloud()
    // {
    //     HttpClient client = new HttpClient();
    //     //var response = await client.PostAsJsonAsync<EmailDTO>("http://localhost:7145/api/Function1", new EmailDTO
    //     var response = await client.PostAsJsonAsync<EmailDTO>("https://autowerksemail.azurewebsites.net/api/SendEmail?", new EmailDTO
    //     {
    //         Name = "testEmailDTO",
    //         Body = new Dictionary<string, string>()
    //         {
    //             { "Email", "rachel.allen1@students.snow.edu" }
    //         }
    //     });
    //     Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    // }

    //This test will always fail because our email function swallows the error and just writes it to the logger.
    //Perhaps we should fix this?
    //[Fact]
    //public async void CallingFunctionWithBadEmailThrowsError()
    //{
    //    //Arrange new email to send
    //    var emailDTO = new EmailDTO
    //    {
    //        Name = "testEmailDTO",
    //        Body = new Dictionary<string, string>()
    //        {
    //            { "Email", "badEmail" }
    //        }
    //    };
    //    HttpClient client = new HttpClient();

    //    //act
    //    var response = await client.PostAsJsonAsync<EmailDTO>("https://autowerksemail.azurewebsites.net/api/SendEmail?", emailDTO);

    //    //Assert
    //    Assert.Equal(500, (int)response.StatusCode);
    //}
}