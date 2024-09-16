using System.Diagnostics.Metrics;

using AutoShopAppLibrary.Components;

using Org.BouncyCastle.Asn1.Ocsp;

using static System.Net.WebRequestMethods;
namespace AutoShopAppLibrary.Telemetry;

public static class WebMeters
{
    //Count How many times each page has been loaded
    public static readonly string MeterName = "AutoWebsiteMeter";
    public static Meter WebsiteMeter = new(MeterName, "1");

    //Home Page loads
    public static Counter<int> HomePageLoads = WebsiteMeter.CreateCounter<int>("Home.Page.Loads");
    public static Counter<int> FAQPageLoads = WebsiteMeter.CreateCounter<int>("FAQ.Page.Loads");
    public static Counter<int> ServicesPageLoads = WebsiteMeter.CreateCounter<int>("Services.Page.Loads");
    public static Counter<int> RequestServicePageLoads = WebsiteMeter.CreateCounter<int>("Request.Service.Page.Loads");

    //Track how long it takes to load the stats page
    public static Histogram<double> HistogramLoadStatsPagetime = WebsiteMeter.CreateHistogram<double>("Stats.Page.Load.Time");
    
    public static ObservableGauge<double> GuageProcessFormSubmitTime = WebsiteMeter.CreateObservableGauge<double>("Form.Submit.Time.Guage.Milliseconds",() => TesterForm.FormSubmitTime);

    /** Generic-Application metrics: **/
    //Track how much memory is being used
    public static ObservableCounter<double> ApplicationMemoryUsage = WebsiteMeter.CreateObservableCounter<double>("Application.Memory.Usage", () => GC.GetTotalMemory(true));

//    //totalrequests
//    public static ObservableCounter<int> ConcurrentRequests = WebsiteMeter.CreateObservableCounter<int>("Total.Http.Requests", () => RequestCounterMiddleware.GetTotalRequestCount)
}