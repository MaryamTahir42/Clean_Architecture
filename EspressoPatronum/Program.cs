using EspressoPatronum.Models.Entities;
using EspressoPatronum.Models.Interfaces;
using EspressoPatronum.Models.Repositories;
using EspressoPatronum.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Dependency Injection Configuration
builder.Services.AddScoped<IUserRepository, UserRepository>();           // User repository
builder.Services.AddScoped<UserService>();                               // User service
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // Generic repository
builder.Services.AddScoped<IGenericRepository<Product>, ProductRepository>();
builder.Services.AddScoped<ProductRepository>(); // Product repository
builder.Services.AddScoped<ProductService>();    // Product service

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
