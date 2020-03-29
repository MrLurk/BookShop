using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.IO;
using System.Text;

namespace Common.Web.Middleware.Log
{
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<RequestLogMiddleware> _logger;

        public RequestLogMiddleware(RequestDelegate next, ILogger<RequestLogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var req = context.Request;
            var reqinfo = $"{req.Scheme}://{req.Host.ToString()}{req.Path.ToString()}{req.QueryString.ToString()}  " +
                $"Body : {new StreamReader(req.Body, Encoding.UTF8).ReadToEndAsync().Result} " +
                $"Auth :{ req.Headers.FirstOrDefault(x => x.Key == "Authorization").Value}";
            _logger.LogInformation(reqinfo);
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }

    public static class RequestLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLog(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogMiddleware>();
        }
    }
}
