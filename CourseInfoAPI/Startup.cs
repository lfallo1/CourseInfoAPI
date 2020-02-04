using System;
using System.Reflection;
using AutoMapper;
using CourseInfoAPI.DbContexts;
using CourseInfoAPI.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CityInfoAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
            })
            .AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            })
            .AddXmlDataContractSerializerFormatters() //enable serializing and deserializing of application/xml
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                //customize body of Validation error responses
                setupAction.InvalidModelStateResponseFactory = (context) =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Type = "http://courseinfoapi.com/modelvalidation",
                        Title = "One or more validation errors occurred",
                        Status = StatusCodes.Status422UnprocessableEntity,
                        Detail = "See errors property for details",
                        Instance = context.HttpContext.Request.Path
                    };

                    problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);
                    return new UnprocessableEntityObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });

            //add AutoMapper to injectable dependencies
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //custom dependencies
            services.AddScoped<ICourseLibraryRepository, CourseLibraryRepository>();

            //services.AddDbContext<CourseLibraryContext>(options =>
            //{
            //    options.UseSqlServer(Environment.GetEnvironmentVariable("COURSE_INFO_SQL_CONNECTION_STRING"));
            //});

            services.AddEntityFrameworkNpgsql().AddDbContext<CourseLibraryContext>(opt =>
                opt.UseNpgsql(Environment.GetEnvironmentVariable("COURSE_INFO_SQL_CONNECTION_STRING")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An error has occurred.  Try again later");
                    });
                });
            }

            //app.UseHttpsRedirection();          

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
