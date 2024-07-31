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
            services.AddScoped<IMenusRepository, MenusRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IMenusRolesRepository, MenusRolesRepository>();
            services.AddScoped<IUsuariosRolesRepository, UsuariosRolesRepository>();
            services.AddControllers();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Seguridad", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Seguridad V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
