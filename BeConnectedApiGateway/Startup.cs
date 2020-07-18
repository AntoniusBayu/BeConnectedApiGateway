using Business;
using DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace BeConnectedApiGateway
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
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddSingleton<IUnitofWork, MongoDBUnitofWork>();
            services.AddSingleton<IConnection, MongoDBConnection>();
            services.AddSingleton<IActivityLog, ActivityLogBusiness>();
            services.AddOcelot(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var config = new OcelotPipelineConfiguration
            {
                // You can do whatever u want laaah...
                // Logging, auth dll
                AuthenticationMiddleware = async (ctx, next) =>
                {
                    var response = Security.IsVerified(ctx, app.ApplicationServices);

                    if (response.Validations.Count > 0)
                    {
                        ctx.Response.StatusCode = 401;
                        ctx.Response.ContentType = "text/html";
                        await ctx.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
                        await ctx.Response.WriteAsync("ERROR!BRO<br><br>\r\n");
                    }
                    else
                    {
                        await next.Invoke();
                    }
                }
            };

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseOcelot(config).Wait();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
