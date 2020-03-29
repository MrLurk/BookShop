using System;
using System.Collections.Generic;
using DbModels;

namespace DAL.Interface
{
    public interface IUserRepository:IRepositoryBase
    {
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        string GetValue();
    }
}
