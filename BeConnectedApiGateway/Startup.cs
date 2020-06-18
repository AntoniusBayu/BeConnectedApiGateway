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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddOcelot(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            var config = new OcelotPipelineConfiguration
            {
                // You can do whatever u want laaah...
                // Logging, auth dll
                AuthenticationMiddleware = async (ctx, next) =>
                {
                    var response = Security.IsVerified(ctx);

                    if (response.Validations.Count > 0)
                    {
                        var json = Helper.JSONSerialize(response);
                        ctx.Response.StatusCode = response.StatusCode;
                        await ctx.Response.WriteAsync(json);

                        ctx.Abort();
                    }

                    await next.Invoke();
                }
            };

            app.UseOcelot(config).Wait();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
