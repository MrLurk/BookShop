using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookShop.Controllers.Tests.Auth;
using DTOModels;
using CSRedis;
using Common.Web.Middleware;
using Common.Mapper;
using DbModels;
using AutoMapper;
using DAL.Achieve;
using DAL.Interface;
using Common.DI;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using Common.Web.Middleware.Log;
using Common.Web.Middleware.Exception;

namespace BookShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region AutoMapper
            services.AddAutoMapperEx(Assembly.Load("DTOModels"));
            services.TryAddSingleton<AutoInjectFactory>();
            #endregion

            #region Swagger
            //注册Swagger生成器，定义一个和多个Swagger 文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Book Shop 对接 API 文档", Version = "v1" });

                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "BookShop.xml");
                c.IncludeXmlComments(xmlPath);

                var DbModelPath = Path.GetDirectoryName(typeof(UserModel).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlDbModelPath = Path.Combine(DbModelPath, "DbModels.xml");
                c.IncludeXmlComments(xmlDbModelPath);
            });

            #endregion

            #region JWT 

            services.Configure<TokenManagement>(Configuration.GetSection("TokenManagement"));
            var token = Configuration.GetSection("TokenManagement").Get<TokenManagement>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                    ValidIssuer = token.Issuer,
                    ValidAudience = token.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                // 使用重写的校验函数
                opt.SecurityTokenValidators.Clear();
                opt.SecurityTokenValidators.Add(new CustomJwtSecurityTokenHandler());
            });

            services.AddScoped<IAuthenticateService, TokenAuthenticationService>();

            #endregion

            #region CSRedis

            var redisConfig = Configuration.GetSection("CacheConfig:RedisInfo").Get<Common.Cache.Redis.RedisConfig>();
            CSRedisClient cSRedisClient = new CSRedisClient(redisConfig.ConnectionString + $",defaultDatabase={redisConfig.DbIndex}");
            RedisHelper.Initialization(cSRedisClient);

            #endregion

            #region DI注入
            services.AddServiceCentralRegistry();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            // AutoMapper添加映射
            app.UseStateAutoMapper();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // 请求日志
            app.UseRequestLog();

            // 启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            // 启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            // 用户异常中间件
            app.UseMiddleware<CusExceptionMiddleware>();
        }
    }
}
