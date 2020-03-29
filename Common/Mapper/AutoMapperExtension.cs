using System;
using System.Reflection;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;

namespace Common.Mapper
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapperEx(this IServiceCollection service,params Assembly[] assemblys)
        {
            service.TryAddSingleton<MapperConfigurationExpression>();

            #region 单个映射

            //service.TryAddSingleton(serviceProvider =>
            //{
            //    var mapperConfigurationExpression = serviceProvider.GetRequiredService<MapperConfigurationExpression>();
            //    var instance = new MapperConfiguration(mapperConfigurationExpression);

            //    instance.AssertConfigurationIsValid();

            //    return instance;
            //});
            #endregion

            service.TryAddSingleton(serviceProvider =>
            {
                var mapperConfiguration = serviceProvider.GetRequiredService<MapperConfiguration>();

                return mapperConfiguration.CreateMapper();
            });

            service.TryAddSingleton(serviceProvider =>
            {
                var mapperConfigurationExpression = serviceProvider.GetRequiredService<MapperConfigurationExpression>();
                var factory = serviceProvider.GetRequiredService<AutoInjectFactory>();
                factory.AddAssemblys(assemblys);

                foreach (var (sourceType, targetType) in factory.ConvertList)
                {
                    mapperConfigurationExpression.CreateMap(sourceType, targetType,MemberList.None);
                }

                var instance = new MapperConfiguration(mapperConfigurationExpression);
                try
                {
                    instance.AssertConfigurationIsValid();
                }
                catch (Exception ex)
                {

                }
                return instance;
            });

            return service;
        }

        public static IMapperConfigurationExpression UseAutoMapper(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.ApplicationServices.GetRequiredService<MapperConfigurationExpression>();
        }
    }
}
