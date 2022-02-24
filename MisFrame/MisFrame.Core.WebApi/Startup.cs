using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Autofac.Extras.DynamicProxy;
using Microsoft.OpenApi.Models;
using MisFrame.Core.WebApi.Aop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using MisFrame.Core.WebApi.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace MisFrame.Core.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region Swagger


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v0.1.0",
                    Title = "Blog.Core API",
                    Description = "框架说明文档"
                });

            });

            #endregion

            /// 添加IMemeryCache注入关系
            services.AddMemoryCache();
            services.AddScoped<ICaching, MemoryCaching>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            #region Swagger

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                c.RoutePrefix = "";
            });

            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        ///  AutoFace 封装
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {

            builder.RegisterType<LogAop>();
            builder.RegisterType<CacheAOP>();

            // 引用Microsoft.DotNet.PlatformAbstractions 类库
            var baseUrl = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;


            // 执行顺序按照注册顺序。
            var aopType = new List<Type>();
            aopType.Add(typeof(LogAop));                       
            aopType.Add(typeof(CacheAOP));                     

            // 仓储load并注册
            var serviceUrl = Path.Combine(baseUrl, "MisFrame.Core.Service.dll");
            var assemblysServices = Assembly.LoadFrom(serviceUrl);
            builder.RegisterAssemblyTypes(assemblysServices)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()       // 注入启用Scope模式
                .EnableInterfaceInterceptors()    // 对目标类型启动拦截器  （需要using Autofac.Extras.DynamicProxy）
                                                  //.InterceptedBy(typeof(LogAop));    // 去掉
                                                  //.InterceptedBy(typeof(CacheAOP));   // 去掉
                .InterceptedBy(aopType.ToArray());


            // 服务load
            var respositoryUrl = Path.Combine(baseUrl, "MisFrame.Core.Repository.dll");
            var assemblysrespositor = Assembly.LoadFrom(respositoryUrl);
            builder.RegisterAssemblyTypes(assemblysrespositor)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();      // 注入启用Scope模式
        }
    }
}
