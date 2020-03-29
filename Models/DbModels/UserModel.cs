using System;
using SqlSugar;namespace DbModels{
    /// <summary>
    /// 模型
    /// </summary>
	[SugarTable("T_User")]	public class UserModel : DBModelBase	{
        public UserModel()
        {

        }
		///<summary>		/// 用户Id		///</summary>        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]        public Int32 Id { get; set; }		///<summary>		/// 钱包余额		///</summary>		public System.Decimal WalletBalance { get; set; }		///<summary>		/// 冻结余额		///</summary>		public System.Decimal LockBalance { get; set; }		///<summary>		/// 手机号码		///</summary>		public String Phone { get; set; }		///<summary>		/// 登陆密码		///</summary>		public String Password { get; set; }		///<summary>		/// 钱包密码		///</summary>		public String PasswordBalance { get; set; }		///<summary>		/// 用户名		///</summary>		public String UserName { get; set; }		///<summary>		/// 登陆名		///</summary>		public String LoginName { get; set; }	}}