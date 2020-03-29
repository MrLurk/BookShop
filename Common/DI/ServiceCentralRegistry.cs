using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.Config;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Common.DI
{
    public static class ServiceCentralRegistry
    {
        public static IServiceCollection AddServiceCentralRegistry(this IServiceCollection service)
        {
            //集中注册服务
            foreach (var item in GetClassName())
            {
                foreach (var typeArray in item.Value)
                {
                    service.AddScoped(typeArray, item.Key);
                }
            }
            // services.AddScoped(typeof(IProductService), typeof(ProductService));//用ASP.NET Core自带依赖注入(DI)注入使用的类
            return service;
        }


        /// <summary>  
        /// 获取程序集中的实现类对应的多个接口
        /// </summary>  
        /// <param name="assemblyName">程序集</param>
        public static Dictionary<Type, Type[]> GetClassName()
        {
            var configList = ConfigHelper.GetListConfig<DIConfigModel>("DIConfigs");
            var result = new Dictionary<Type, Type[]>();
            foreach (var config in configList)
            {
                if (!String.IsNullOrEmpty(config.AssemblyName))
                {
                    try
                    {
                        //var pathPrefix = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                        //Assembly assembly = Assembly.LoadFile(Path.Combine(pathPrefix, config.AssemblyName));
                        Assembly assembly = Assembly.Load( config.AssemblyName);
                        List<Type> ts = assembly.GetTypes().Where(x => x.Namespace.Contains(config.Namespace)).ToList();

                        foreach (var item in ts.Where(s => !s.IsInterface))
                        {
                            var interfaceType = item.GetInterfaces();
                            result.Add(item, interfaceType);
                        }
                    }
                    catch (Exception ex)
                    {
                        // 配置读出来反射异常，直接下一个
                        continue;
                    }
                }
            }
            return result;
        }
    }
}
