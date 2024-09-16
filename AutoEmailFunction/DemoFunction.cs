using MailKit.Security;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AutoEmailFunction
{
    public class DemoFunction
    {
        private readonly ILogger<DemoFunction> _logger;
        private readonly IEmailService _emailService;

        public DemoFunction(ILogger<DemoFunction> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [Function("ManagerEmailFunction")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {

            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}