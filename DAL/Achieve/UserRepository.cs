using System;
using System.Collections.Generic;
using DAL.Interface;
using DbModels;

namespace DAL.Achieve
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository()
        {

        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        public string GetValue()
        {
            /*查询*/
            var list = db.Queryable<UserModel>().ToList();//查询所有
            var res = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            return res;
        }
    }
}
