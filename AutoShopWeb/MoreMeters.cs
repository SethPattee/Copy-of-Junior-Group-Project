using System.Diagnostics.Metrics;

using AutoShopAppLibrary.Telemetry;
namespace AutoShopWeb;

public class MoreMeters
{
    //totalrequests
    public static ObservableCounter<int> HttpRequestsCount = WebMeters.WebsiteMeter.CreateObservableCounter<int>("Total.Http.Requests", () => RequestCounterMiddleware.GetTotalRequestCount());
}