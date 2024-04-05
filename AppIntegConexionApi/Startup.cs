using AppIntegConexionCore.Interfaces;
using AppIntegConexionCore.Repository;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AppIntegConexionApi
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
            services.AddScoped<IControlSesionRepository,ControlSesionRepository>();
            services.AddScoped<IConexionesRepository, ConexionesRepository>();
            services.AddScoped<IUsuariosRepository, UsuariosRepository>();
            services.AddControllers();
            services.AddSingleton<IConfiguration>(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
