using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq;
using Common.Convert;
using System.Collections.Generic;

namespace Common.Config
{

    public static class ConfigHelper
    {
        private static IConfiguration _configuration;

        static ConfigHelper()
        {
            //在当前目录或者根目录中寻找appsettings.json文件
            var fileName = "appsettings.json";

            var directory = AppContext.BaseDirectory;
            directory = directory.Replace("\\", "/");

            var filePath = $"{directory}/{fileName}";
            if (!File.Exists(filePath))
            {
                var length = directory.IndexOf("/bin");
                filePath = $"{directory.Substring(0, length)}/{fileName}";
            }

            var builder = new ConfigurationBuilder()
                .AddJsonFile(filePath, false, true);

            _configuration = builder.Build();
        }

        public static string GetSectionValue(string key)
        {
            return _configuration.GetSection(key).Value;
        }


        /// <summary>
        /// 获取配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetConfig<T>(string key)
            where
            T : class
        {
            var children = _configuration.GetSection(key).GetChildren();

            var props = typeof(T).GetProperties();
            var tResult = Activator.CreateInstance<T>();

            foreach (var item in children)
            {
                var prop = props.FirstOrDefault(x => x.Name == item.Key);
                if (prop == null)
                    continue;
                // item.Value 需要转类型
                prop.SetValue(tResult, ReflectionTypeConvert.Convert(prop.PropertyType,item.Value));
            }
            return tResult;
        }


        /// <summary>
        /// 获取 List 配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<T> GetListConfig<T>(string key)
            where
            T : class
        {
            var children = _configuration.GetSection(key).GetChildren();
            List<T> list = new List<T>();

            foreach (var item in children)
            {
                var obj= item.Get<T>();
                list.Add(obj);
            }
            return list;
        }

    }
}