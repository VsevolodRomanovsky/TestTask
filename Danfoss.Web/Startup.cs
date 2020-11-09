using Danfoss.Contracts;
using Danfoss.DataLayer;
using Danfoss.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using System;
using Danfoss.Services.Common.Validation;
using Danfoss.Entities;
using Danfoss.Web.ActionFilters;
using Newtonsoft.Json.Converters;

namespace Danfoss.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void AddSwaggerConfiguration(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Danfoss TetTask Service API",
                    Version = "v1",
                    Description = "Test Task",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlPath);
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }

        private void AddDbContext(IServiceCollection services)
        {
            var connectionString = Configuration["DbContextSettings:ConnectionString"];
            string assemblyName = typeof(DanfossDbContext).Namespace;
            services.AddDbContext<DanfossDbContext>(options =>
              options.UseSqlServer(connectionString,
                  optionsBuilder =>
                      optionsBuilder.MigrationsAssembly(assemblyName)
              ));
        }

        private void RegisterServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICounterService, CounterService>();
            serviceCollection.AddTransient<IHouseSrvice, HouseService>();
        }

        private void RegisterModelValidators(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IModelValidator<House>, HouseNewValidator>();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.WithOrigins(Configuration["CorsSettings:ngClientUrl"]));
            });

            serviceCollection.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                }
            );
            serviceCollection.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateActionParametersAttribute));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            serviceCollection.AddControllers();
            RegisterServices(serviceCollection);
            RegisterModelValidators(serviceCollection);
            AddSwaggerConfiguration(serviceCollection);

            //Using SQL Server
            AddDbContext(serviceCollection);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(builder => builder.WithOrigins(Configuration["CorsSettings:ngClientUrl"]).AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "Danfoss Test Task Services"));
        }
    }
}
