using Common.Web.Middleware.Log;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Common.Web.Middleware.Exception
{
    /// <summary>
    /// 异常捕获中间件
    /// </summary>
    public class CusExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLogMiddleware> _logger;

        public CusExceptionMiddleware(RequestDelegate next, ILogger<RequestLogMiddleware> logger)
        {
            _next = next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CusException ex)
            {
                httpContext.Response.StatusCode = 200;
                _logger.LogError(ex.Message);
                using (var writer = new StreamWriter(httpContext.Response.Body, Encoding.UTF8))
                {
                    await writer.WriteAsync(JsonConvert.SerializeObject(new 
                    {
                        code = "err",
                        msg = ex.Message
                    }));
                }
            }
            catch (System.Exception ex)
            {
                httpContext.Response.StatusCode = 200;
                _logger.LogError(ex,ex.Message);
                using (var writer = new StreamWriter(httpContext.Response.Body, Encoding.UTF8))
                {
                    await writer.WriteAsync(JsonConvert.SerializeObject(new 
                    {
                        code = "err",
                        msg = ex.Message
                    }));
                }
            }
        }
    }
}
