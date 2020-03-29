using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookShop.Controllers.Tests.Log
{
    [Route("api/tests/nlog")]
    public class NlogTestController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        public NlogTestController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: api/values
        [HttpGet]
        public ActionResult Get()
        {
            _logger.LogError("错误信息：LogError");
            _logger.LogDebug("调试信息：LogDebug");
            _logger.LogInformation("详细信息：LogInformation");
            _logger.LogWarning("警告信息：LogWarning");

            return Ok(1);
        }
    }
}
