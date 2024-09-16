
using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace AutoShopWeb;


public class RequestCounterMiddleware
{
    private readonly RequestDelegate _next;
    private static int _requestCount = 0;

    public RequestCounterMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Increment the request count for each incoming HTTP request
        _requestCount++;

        // Call the next delegate/middleware in the pipeline
        await _next(context);
    }

    public static int GetTotalRequestCount()
    {
        return _requestCount;
    }
}