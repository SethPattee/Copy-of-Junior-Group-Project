using System.Diagnostics.Metrics;

using AutoShopAppLibrary.Components;
using AutoShopAppLibrary.Data;
using AutoShopAppLibrary.Services;
using AutoShopAppLibrary.Shared;
using AutoShopAppLibrary.Telemetry;

using AutoShopWeb;
using AutoShopWeb.Components;
using AutoShopWeb.Services;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);
var serviceName = "MyCompany.MyProduct.MyService";
var serviceVersion = "1.0.0";
// Add services to the container.

builder.Services.AddLogging();

builder.Logging.AddOpenTelemetry(options =>
{
    options
        .SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService(serviceName))
        .AddConsoleExporter()
        .AddOtlpExporter(opt =>
         {
             //opt.Endpoint = new Uri(builder.Configuration["COLLECTOR_URL"] ?? throw new NullReferenceException("Enviroment not set"));
             opt.Endpoint = new Uri("http://otel-collector:4317"); //changed this one 
         });
});

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(serviceName))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddSource(TraceClass.SubmitFormTraceName)
        .ConfigureResource(resource => resource.AddService(serviceName: TraceClass.SubmitFormTraceName,
        serviceVersion: TraceClass.SubmitFormTraceVersion))
        //last time, there was another trace here, but i think only the one was necesary
        //the line below i think is important
        //.AddOtlpExporter(opt =>
        //{
        //    opt.Endpoint = new Uri(builder.Configuration["COLLECTOR_URL"] ?? throw new NullReferenceException("environment variable for collector url not set or found: COLLECTOR_URL"));
        //})
        .AddConsoleExporter())
    .WithMetrics(metrics => metrics
    .AddMeter(WebMeters.MeterName) //in our Shared Class Library
    .AddMeter(WebMeters.RequestServicePageLoads.Name) //Shared Class Library, it made things work I think?
    .AddMeter(MoreMeters.HttpRequestsCount.Name) //because it's declared in our web app, not than in the app library
    .AddAspNetCoreInstrumentation()
    .AddPrometheusExporter()
    .AddOtlpExporter(opt =>
    {
        opt.Endpoint = new Uri("http://otel-collector:4317/");
    }));

using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddOpenTelemetry(logging =>
    {
        logging.AddOtlpExporter();
    });
});

//Rachel Note: might need more lines about logging here (see demo from TicketsProject telemetry)

builder.Services.AddSwaggerGen();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddDbContextFactory<PostgresContext>(c => c.UseNpgsql(builder.Configuration["dbConnection"]));
builder.Services.AddScoped<IntakeService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddSingleton<IDataService, WebDataService>();
builder.Services.AddScoped<DBService>();
builder.Services.AddScoped<IDBService, DBService>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//for telemetry
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.UseMiddleware<RequestCounterMiddleware>(); //I added this to count the total number of HTTP requests we receive


app.UseHttpsRedirection();
app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});

app.MapHealthChecks("/health", new HealthCheckOptions
{
    AllowCachingResponses = false,
    ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
});

app.UseStaticFiles();
app.UseAntiforgery();
app.MapControllers();
app.MapPrometheusScrapingEndpoint();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(NavBanner).Assembly);

app.Run();
public partial class Program { }