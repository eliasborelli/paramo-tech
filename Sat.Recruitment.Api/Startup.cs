using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Filters;
using Sat.Recruitment.Api.Mappings;
using Sat.Recruitment.Api.Validators;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Core.Settings;
using Sat.Recruitment.Infraestructure.Commons;
using Sat.Recruitment.Infraestructure.Repositories;
using Sat.Recruitment.Services.Interfaces;
using Sat.Recruitment.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api
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
            services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()))
                    .ConfigureApiBehaviorOptions(options =>
                    {
                        options.SuppressModelStateInvalidFilter = true;
                    })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserValidator>());

            services.AddSwaggerGen();
            services.Configure<Settings>(Configuration.GetSection(nameof(Settings)));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserMapping, UserMapping>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseRouting();

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
