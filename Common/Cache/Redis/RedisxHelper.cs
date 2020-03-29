using Common.Config;
using CSRedis;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Cache.Redis
{
    public class RedisxHelper
    {
        private readonly static RedisConfig _RedisConfig = ConfigHelper.GetConfig<Common.Cache.Redis.RedisConfig>("CacheConfig:RedisInfo");
        /// <summary>
        /// 不常用，建议使用 CsRedis 自带的 RedisHelper
        /// </summary>
        public readonly static CSRedisClient Redis = new CSRedisClient(_RedisConfig.ConnectionString + $",defaultDatabase={_RedisConfig.DbIndex}");

        /***
         * 如果需要使用多个 Db，可以在这里建立一个工厂，来实现不同的参数返回不同的 RedisClint 实例
         */
    }

    public static class CSRedisEx
    {
        /// <summary>        /// 添加hash表(批量)        /// </summary>        /// <typeparam name="T">值模型</typeparam>        /// <param name="key">键</param>        /// <param name="keyValues">哈希表键值对</param>        /// <returns>true成功,false失败</returns>        public static bool SetHashTables<T>(this CSRedisClient cSRedis, string key, Dictionary<string, T> keyValues)
        {
            var insertParams = new List<object>();
            foreach (var item in keyValues)
            {
                insertParams.Add(item.Key);
                insertParams.Add(item.Value);
            }
            return cSRedis.HMSet(key, insertParams.ToArray());
        }

    }

}