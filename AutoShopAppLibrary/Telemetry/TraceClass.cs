using System.Diagnostics;

namespace AutoShopAppLibrary.Telemetry;

public class TraceClass
{
    //To track the process of how long it takes to submit a form
    public static readonly string SubmitFormTraceName = "SubmitFormTrace";
    public static readonly string SubmitFormTraceVersion = "1";
    public static readonly ActivitySource SubmitFormTrace = new(SubmitFormTraceName);

    //ideas:
    //track how long it takes to load the stats page? (querying data from the database)

    //track how many people actually click on the map
    //how many people scroll through the news feed
    //trace just how long it takes from when we call the azure function to when we get a response for emails
    //alternatively, stick a trace in the Azure function itself, but this might add too many layers of complication.

    //for generic meters: track time spent on each page?

    //I will use this in a minute: Probably put it in the 
    //using var myActivity = TraceClass.SubmitFormTrace.StartActivity("ViewFormPage");
}