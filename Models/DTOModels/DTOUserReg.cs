using System;
using CommonModels;
using DbModels;

namespace DTOModels
{
    [AutoInject(typeof(UserModel), typeof(DTOUserReg))]
    public class DTOUserReg: DTOModelBase
	{		///<summary>		/// 用户名		///</summary>		public String UserName { get; set; }
		///<summary>		/// 登陆密码		///</summary>        public String Password { get; set; }
	}
}
