using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Net.Http.Json;

using AutoShopAppLibrary.Data;
using AutoShopAppLibrary.Services;
using AutoShopAppLibrary.Shared;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AutoShopAppLibrary.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration config, ILogger<EmailService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task SendEmail(EmailDTO email)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response1 = await client.PostAsJsonAsync("https://autowerksemail.azurewebsites.net/api/SendEmail", email);
                }
                catch (Exception ex)
                {
                    using (HttpClient secondClient = new HttpClient())
                    {
                        _logger.LogError(ex.ToString());
                        await secondClient.PostAsJsonAsync("http://localhost:7145/api/SendEmailB", email);
                    }
                }
            }
        }
    }
}






/*using AutoShopAppLibrary.Data;
using AutoShopAppLibrary.Services;
using AutoShopAppLibrary.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Net.Http.Json;

namespace AutoShopAppLibrary.Services;

public class EmailService() : IEmailService
{
    //private readonly HttpClient _httpClient;
    private IConfiguration _config;

    //public EmailService()
    //{
    //    _httpClient = new HttpClient();
    //}
    public EmailService(IConfiguration config)
    {
        _config = config;
    }
    public async Task SendEmail(EmailDTO email)
    {
        using(HttpClient client = new HttpClient())
        {
            try
            {
                client.BaseAddress = new Uri(config["ApiAddress"]);
                await client.PostAsJsonAsync("/DB", email);

            }
            catch (Exception ex)
            {
                

                await client.PostAsJsonAsync("https://localhost:7237/DB", email);
            }
        }

    }
}
*/