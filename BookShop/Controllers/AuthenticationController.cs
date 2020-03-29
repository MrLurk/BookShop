using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.Controllers.Tests;
using BookShop.Controllers.Tests.Auth;
using Common.Cache;
using Common.Cache.Redis;
using Common.Config;
using Common.Convert;
using DAL;
using DbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SqlSugar;
using System.Security.Claims;
using Common.Web;

namespace BookShop.Controllers
{
    [Route("api/tests/auth")]
    [ApiController, Authorize]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthenticateService _authService;
        public AuthenticationController(IAuthenticateService authService)
        {
            this._authService = authService;
        }

        [HttpPost, Route("RequestToken"),AllowAnonymous]
        public ActionResult RequestToken(LoginRequestDTO request)
        {
            if (_authService.IsAuthenticated(request, out string token))
            {
                return Ok(token);
            }
            return BadRequest("Invalid Request");
        }

        [HttpGet, Route("RefreshToken")]
        public ActionResult RefreshToken()
        {
            var ttl = RedisHelper.Ttl(RedisPrefix.User_Login_Token_Key.GetKey(User.GetUserId()));
            if (ttl <= ConfigHelper.GetSectionValue("TokenManagement:RefreshExpiration").ToInt() * 60)
            {
                _authService.IsAuthenticated(User.Claims, out string token);
                return Ok(new { ttl, refreshToken = token });
            }
            return Ok(new { ttl, refreshToken = string.Empty });
        }

    }
}
