using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Interface;
using DTOModels;

namespace BookShop.Controllers.User
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserLogic _userLogic = null;

        public UserController(IUserLogic userLogic)
        {
            this._userLogic = userLogic;
        }

        [HttpPost("UserReg")]
        public Object UserReg(DTOUserReg dto)
        {
            var c = _userLogic.UserReg(dto);
            return c;
        }
    }
}
