using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;

public class UrlLoggerMiddleware 
{
    private readonly RequestDelegate _next;
    private readonly ILogger<UrlLoggerMiddleware> _logger;
    
    public UrlLoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<UrlLoggerMiddleware>()
            ?? throw new ArgumentNullException(nameof(loggerFactory));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation($"Request URL: {UriHelper.GetDisplayUrl(context.Request)}");
        await _next(context);
    }
}