using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModalsWithRouting
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        // Startup
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }




        // Configure Services ================================================================================= 
        public void ConfigureServices(IServiceCollection services)// This method gets called by the runtime. Use this method to add services to the container.// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        {
        }





        // Configure ===========================================================================================
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        {
            // Dev - Mode
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // If 404 Not found - show home
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Response.Redirect("/");
                    return;   // short circuit
                }
            });


            // If 404 Not found - show home
            app.Use(async (context, next) =>
            {
                var url = context.Request.Path.Value;
                // Rewrite to index
                if (url.Contains("/home/privacy"))
                {
                    // rewrite and continue processing
                    context.Request.Path = "/htmlViews/firstModal.html";
                }
                await next();
            });


            // SPA
            app.UseDefaultFiles(); // Means search for the index.html or default files in wwwroot - 
            app.UseStaticFiles(); // Static files are files in the wwwroot - Use wwwroot files


            // Https
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseRouting();


            // Endpoints
            app.UseEndpoints(endpoints => // Route to the index.html if 404 Error and for SPA it always routes to index because SPA is single page APP.  // The url routing is done on CLient side 
            {
                endpoints.MapFallbackToFile("/index.html");

                //endpoints.MapGet("/htmlViews/{name:*}", async context =>
                //{
                //    var name = context.Request.RouteValues["name"];
                //    await context.Response.WriteAsync($"Hello {name}!");
                //});
            });


           

        }
    }
}
