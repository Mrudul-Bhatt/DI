using DI.Models;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//this will map the values from weatherApi in appsettings.json to the WeatherApiOptions class
builder.Services.Configure<WeatherApiOptions>(builder.Configuration.GetSection("weatherApi"));

//Add service to DI container. Since we cannot create objects of interface, we add the Contract(Interface) and its implementation(Service) to DI container. This is called Dependency Injection. So at runtime when we need an object of ICitiesService, we will get an object of CitiesService. This is called Inversion of Control and we get the interface object in the constructor param

//builder.Services.Add(new ServiceDescriptor(typeof(ICitiesService), typeof(CitiesService), ServiceLifetime.Singleton));

builder.Services.AddTransient<ICitiesService, CitiesService>();

var app = builder.Build();

//if app is in development then show the exception and stack trace on browser, else show the friendly error page
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//accessing the Configuration property MyKey, which is in appsettings.json file, if not present then creating a default value 
//app.Configuration.GetValue<string>("MyKey", "default value");

app.UseStaticFiles();
app.MapControllers();

app.Run();
