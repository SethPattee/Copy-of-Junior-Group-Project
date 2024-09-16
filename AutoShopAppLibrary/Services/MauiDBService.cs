using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using AutoShopAppLibrary.Data;

using Microsoft.Extensions.Logging;

namespace AutoShopAppLibrary.Services;

public class MauiDBService : IDBService
{
    private readonly ILogger<MauiDBService> _logger;

    public MauiDBService(ILogger<MauiDBService> logger)
    {
        _logger = logger;
    }
    public Task PlaceCustomerInDB(Customer customer)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> StoreCustomerDetailsInDatabase(EmailDTO customerDetails)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                var response1 = await client.PostAsJsonAsync("https://fngautoweb.azurewebsites.net/FormDetails", customerDetails);
                return true;
            }
            catch (Exception ex)
            {
                using (HttpClient secondClient = new HttpClient())
                {
                    _logger.LogError(ex.ToString());
                    await secondClient.PostAsJsonAsync("https://fngautoweb.azurewebsites.net/FormDetails", customerDetails);
                }
                return false;
            }
        }
    }

    public Task<bool> UpdateCustomerDetailsInDatabase(EmailDTO customerDetails)
    {
        throw new NotImplementedException();
    }
}