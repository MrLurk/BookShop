using System;
using System.Collections.Generic;
using System.Linq;
using Common.Cache.Redis;
using Common.Convert;
using Common.Mapper;
using DAL.Interface;
using DbModels;
using DTOModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class HomeController : ControllerBase
    {

        public readonly IBookRepository bookRepository = null;
        public readonly IUserRepository userRepository = null;
        public HomeController(IBookRepository bookRepository, IUserRepository userRepository)
        {
            this.bookRepository = bookRepository;
            this.userRepository = userRepository;
        }
        /// <summary>
        /// 测试代码
        /// </summary>
        /// <param name="userId">测试 Id</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserModel), 200)]
        public IEnumerable<string> Get(int userId)
        {
            var list = bookRepository.GetList();
            var dbuser = new UserModel() { Id = 1, LockBalance = 100, LoginName = "张三", Password = "123", PasswordBalance = "123456", Phone = "13200000001", UserName = "张三的账户", WalletBalance = 1001 };
            var c = dbuser.MapTo<DTOUserReg>();

            var res = bookRepository.GetValue();
            var res2 = userRepository.GetValue();


            return new string[] { res, res2, c.ToJson() };
        }

        // POST: api/Home
        [HttpPost]
        public string Post(string value)
        {
            return new { a = 1, b = 2, c = DateTime.MaxValue }.ToJson();
        }

        // PUT: api/Home/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }



}
