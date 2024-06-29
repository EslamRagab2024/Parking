using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Parking.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "mycors",
                      policy =>
                      {
                          policy.WithOrigins("http://127.0.0.1:5000");
                      });
});
builder.Services.AddRazorPages();
//builder.Services.AddIdentity<IdentityUser, IdentityRole>
//    (options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MyDb>()
//    .AddDefaultTokenProviders();

builder.Services.AddDbContext<MyDb>(options =>
{
    options.UseSqlServer("Data Source =.; Initial Catalog = parkingdb1; Integrated Security = True");
});
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//app.UseCors("mycors");


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Login}/{id?}");
});

app.Run();
