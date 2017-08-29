using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DTuriasData.Models;
using Microsoft.Extensions.Logging;
using System;
using DTuriasData.Utils;
using DTuriasData.Controllers;
using Newtonsoft.Json.Serialization;

namespace DTuriasData
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
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddLogging(builder => builder                                               
                            .AddFilter("TweetValuesController", LogLevel.Trace)
                            .AddFilter("Microsoft", LogLevel.Error)
                            .AddFilter("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.Error)
                            .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Error)
                            .AddFilter("System", LogLevel.Error)
                            .AddFilter("Engine", LogLevel.Error)
                            //.AddFilter("Default", LogLevel.Error)
                            .AddConsole());
            //.AddFilter(LoggingEvents.RESTFulAPI)
            //.AddDebug());

            var mvcCore = services.AddMvcCore()
                .AddCors();
            mvcCore.AddJsonFormatters(options => options.ContractResolver = new CamelCasePropertyNamesContractResolver());
        }
        // .AddCors();
    //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowCredentials();
                builder.AllowAnyOrigin(); 
            });

            app.UseMvc();


            //Comment before creating a migration
            //var dataContext = serviceProvider.GetService<DataContext>();
            //Models.SeedData.SeedDatabase(dataContext);
        }
    }
}
