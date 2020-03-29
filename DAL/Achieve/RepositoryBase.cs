using System;
using System.Linq;
using Common.Config;
using SqlSugar;

namespace DAL.Achieve
{
    public class RepositoryBase
    {
        protected SqlSugarClient db = null;
        public RepositoryBase()
        {

            var dbConnectionString = ConfigHelper.GetSectionValue("DbConnnections:DefaultDb");
            db = new SqlSugarClient(
            new ConnectionConfig()
            {
                ConnectionString = dbConnectionString,
                DbType = DbType.SqlServer,//设置数据库类型
                IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
            });

            //用来打印Sql方便你调式    
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
        }

    }
}
