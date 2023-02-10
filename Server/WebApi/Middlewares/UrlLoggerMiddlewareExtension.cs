using Microsoft.AspNetCore.Builder;

public static class UrlLoggerMiddlewareExtension 
{
    public static IApplicationBuilder UseUrlLogger(this IApplicationBuilder application)
    {
        return application.UseMiddleware<UrlLoggerMiddleware>();
    }
}