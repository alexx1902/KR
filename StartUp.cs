using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApiClean.Data;

namespace DataAccessLayer;

public class Startup
{
    public Startup(IConfiguration conf)
    {
        Configuration = conf;
    }
    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services)
    {
        string con = "server=DESKTOP-364AU0U\\SQLEXPRESS;database=KR;trusted_connection=true";
        services.AddDbContext<Context>(options => options.UseSqlServer(con));
        services.AddControllers();
      
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "FitnessApp", Version = "v1" });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FitnessApp v1.0"));
        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}