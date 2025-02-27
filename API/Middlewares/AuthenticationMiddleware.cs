using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // List of paths to bypass authentication
        var bypassPaths = new[] { "/login", "/register" };

        // Check if the request path is in the bypass list
        if (bypassPaths.Contains(context.Request.Path.Value.ToLower()))
        {
            await _next(context);
            return;
        }

        // Check if the request has an Authorization header
        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Authorization header missing");
            return;
        }

        var token = context.Request.Headers["Authorization"].ToString();
        bool isValidToken = false;


        /*
            VALIDATION LOGIC
        */
        
        if (isValidToken)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid token");
            return;
        }

        // Call the next middleware in the pipeline
        await _next(context);
    }
}

// Extension method to add the middleware to the HTTP request pipeline
public static class AuthenticationMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomAuthentication(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthenticationMiddleware>();
    }
}