using System;
using System.Collections.Generic;
using DbModels;

namespace DAL.Interface
{
    public interface IBookRepository:IRepositoryBase
    {
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        string GetValue();


        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        IList<BookModel> GetList();
    }
}
