using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//Add service to DI container. Since we cannot create objects of interface, we add the Contract(Interface) and its implementation(Service) to DI container. This is called Dependency Injection. So at runtime when we need an object of ICitiesService, we will get an object of CitiesService. This is called Inversion of Control and we get the interface object in the constructor param
builder.Services.Add(new ServiceDescriptor(typeof(ICitiesService), typeof(CitiesService), ServiceLifetime.Transient));

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();
