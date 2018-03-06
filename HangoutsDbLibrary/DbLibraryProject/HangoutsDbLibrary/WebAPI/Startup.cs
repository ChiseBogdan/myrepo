using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using HangoutsDbLibrary.Data;
using WebAPI.Services;
using WebAPI.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;

namespace WebAPI
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
            services.AddCors();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>()
                                           .ActionContext;
                return new UrlHelper(actionContext);
            });


            services.AddScoped<IServiceGroup, ServiceGroup>();
            services.AddScoped<IServiceUser, ServiceUser>();
            services.AddScoped<IServiceUserGroup, ServiceUserGroup>();
            services.AddScoped<IServiceActivity, ServiceActivity>();
            services.AddScoped<IServiceLocation, ServiceLocation>();
            services.AddScoped<IServiceInterest, ServiceInterest>();
            services.AddScoped<IServicePlan, ServicePlan>();
            services.AddScoped<IServiceGroupActivity, ServiceGroupActivity>();

            services.AddScoped<IInterestMapper, InterestMapper>();
            services.AddScoped<IPlanMapper, PlanMapper>();
            services.AddScoped<IGroupAdminMapper, GroupAdminMapper>();
            services.AddScoped<ILocationMapper, LocationMapper>();
            services.AddScoped<IActivityMapper, ActivityMapper>();
            services.AddScoped<IGroupMapper, GroupMapper>();
            services.AddScoped<IUserMapper, UserMapper>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            DesignTimeDbContextFactory designTimeDbContextFactory = new DesignTimeDbContextFactory();
            HangoutsContext context = designTimeDbContextFactory.CreateDbContext(new String[] { });
            DbInitializer.Initialize(context);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseMvc();
        }
    }
}
