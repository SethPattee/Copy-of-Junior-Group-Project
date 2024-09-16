using AutoEmailFunction;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

/*var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<IEmailService, IntakeService> ();
        
    })
    .Build();

host.Run();
*/

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    //.ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((context, config) =>
    {
        if (context.HostingEnvironment.IsDevelopment())
        {
            var userSecretsBuilder = new ConfigurationBuilder();
            userSecretsBuilder.AddUserSecrets<Program>();
            var userSecretsConfig = userSecretsBuilder.Build();
            config.AddConfiguration(userSecretsConfig);
        }
    })
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<IEmailService, IntakeService>();
    })
    .Build();

host.Run();