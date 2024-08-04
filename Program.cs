using JewelleryStoreManagementSystem.Data;
using JewelleryStoreManagementSystem.Data.Models;
using JewelleryStoreManagementSystem.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<JewelleryStoreManagementSystemContext>(options =>
{
    var folder = Environment.SpecialFolder.LocalApplicationData;
    var path = Environment.GetFolderPath(folder);
    var dbPath = System.IO.Path.Join(path, "JewelleryStore.db");
    options.UseSqlite($"Data Source={dbPath}");
});

builder.Services.AddScoped<AdminRepository>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<InventoryRepository>();
builder.Services.AddScoped<OrderItem>();
builder.Services.AddScoped<Order>();
builder.Services.AddScoped<Product>();
builder.Services.AddScoped<Review>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
