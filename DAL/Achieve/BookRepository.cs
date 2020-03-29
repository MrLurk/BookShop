using System;
using System.Collections.Generic;
using DAL.Interface;
using DbModels;

namespace DAL.Achieve
{
    public class BookRepository : RepositoryBase, IBookRepository
    {
        public BookRepository()
        {

        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        public string GetValue()
        {
            /*查询*/
            var list = db.Queryable<BookModel>().ToList();//查询所有
            var res = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            return res;
        }


        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        public IList<BookModel> GetList()
        {
            /*查询*/
            var list = db.Queryable<BookModel>().ToList();//查询所有
            return list;
        }
    }
}
