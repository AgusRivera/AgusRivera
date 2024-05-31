using AMVTravels.Extensions;
using Serilog.Events;
using Serilog;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using AMVTravels.Contexts;

const string proyectName = "AMVTravels";

//! En caso que la aplicación genere una excepción al iniciar, esta va a ser capturada y grabada en un archivo log.
Log.Logger = new LoggerConfiguration()
    .WriteTo.Debug()
    .WriteTo.File("Logs/log-.log", LogEventLevel.Error, rollingInterval: RollingInterval.Day)
    .CreateBootstrapLogger();

try
{
    Log.Information(">>>>> {nombreProyecto}: Iniciando la aplicación.", proyectName);

    var builder = WebApplication.CreateBuilder(args);

    //! Configuración de Serilog
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services));

    // Utilización del appsettings correspondiente al entorno de ejecución.
    builder.Configuration.AddJsonFile($"appsattings.{builder.Environment.EnvironmentName}", reloadOnChange: true, optional: true);
    builder.Services.AddControllersWithViews().AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        opt.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

    builder.Services.AddDbContext<AppDbContext>(config => config.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

   builder.Services.AddDbContext<IdentityDBContext>(config => config.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = proyectName,
                Version = "v1",
                Description = "Proyect Description",
            });
        });
    }

    builder.AddServices();

    var app = builder.Build();


    if (app.Environment.IsDevelopment())
    {
        //! Configuración de los endpoints y la url de swagger.
        app.UseSwagger();
        app.UseSwaggerUI(opt =>
        {
            opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            //opt.RoutePrefix = "docs";
        });
    }
    else
    {
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseSerilogRequestLogging();
    app.UseRouting();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();


}
catch(Exception ex)
{
    Log.Fatal(ex, ">>>>> La aplicacion genero una excepción al intentar iniciar.");
}
finally
{
    Log.CloseAndFlush();
}
