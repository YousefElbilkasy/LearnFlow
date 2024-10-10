using LearnFlow.Data;
using LearnFlow.Interfaces;
using LearnFlow.Models;
using LearnFlow.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserRepo, UserRepo>();

// Configure Entity Framework with SQL Server
builder.Services.AddDbContext<LearnFlowContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// Add Identity services
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>() // Make sure this matches your context
//    .AddDefaultTokenProviders();

// Configure cookie authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Path to the login page
    options.LogoutPath = "/Account/Logout"; // Path to the logout page
    options.AccessDeniedPath = "/Account/AccessDenied"; // Path for access denied
    options.SlidingExpiration = true; // Optional: enable sliding expiration
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Optional: set expiration time
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable authentication and authorization
app.UseAuthentication(); // Make sure this is before UseAuthorization()
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();