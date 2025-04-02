using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using VehicleApp.DAL;
using VehicleApp.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Dodavanje DbContext-a s PostgreSQL-om
builder.Services.AddDbContext<VehicleContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection")));

// AutoMapper konfiguracija
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Autofac konfiguracija
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DIConfig(builder.Configuration));
});

//ReactApp cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:3000") 
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseCors("AllowReactApp");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();