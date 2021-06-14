using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CusMiddlewarre
{
    public class BrowserCheckMiddleware
    {
        private RequestDelegate _next;
        public BrowserCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            if (context.Request.Path == "/")
            {
                string headers = context.Request.Headers["User-Agent"].ToString();
                if (headers.Contains("Edg") == true)
                {
                    await context.Response.WriteAsync($"<p>{headers}<p>");

                    await context.Response.WriteAsync("<p>You cant use this application in that browser!! </p>");
                    await context.Response.WriteAsync("<p>Use please Chrome/Firefox/Opera </p>");
                }
                await _next(context);
            }
            await _next(context);
        }
    }

    public static class BrowserCheckMiddlewareExtensions
    {
        public static IApplicationBuilder UseCheckBrowserMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BrowserCheckMiddleware>();
        }
    }
}
