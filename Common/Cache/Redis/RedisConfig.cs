using System;
namespace Common.Cache.Redis
{
    public class RedisConfig
    {
        /// <summary>
        /// 默认 Db 索引
        /// </summary>
        public int DbIndex { get; set; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
