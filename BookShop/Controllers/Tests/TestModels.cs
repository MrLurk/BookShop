using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BookShop.Controllers.Tests
{


    /// <summary>
    /// 测试登陆
    /// </summary>
    public class LoginRequestDTO
    {
        [JsonIgnore]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
