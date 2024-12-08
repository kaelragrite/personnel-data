using System.Globalization;

namespace PersonnelData.Config.Middlewares;

public class AcceptLanguageMiddleware
{
    private readonly RequestDelegate _next;

    public AcceptLanguageMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var culture = context.Request.Headers["Accept-Language"].ToString();
        if (string.IsNullOrWhiteSpace(culture) || culture.Contains("en-us", StringComparison.InvariantCultureIgnoreCase))
            culture = "ka";

        var cultureInfo = new CultureInfo(culture);
        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;
        
        await _next(context);
    }
}