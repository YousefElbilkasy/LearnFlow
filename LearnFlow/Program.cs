using LearnFlow.Data;
using LearnFlow.Interfaces;
using LearnFlow.Models;
using LearnFlow.Repositories;
using LearnFlow.Repository;
using LearnFlow.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using test_video_2.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddDbContext<LearnFlowContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
// Add Identity
builder.Services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<LearnFlowContext>()
                .AddDefaultTokenProviders();
builder.Services.AddScoped<ILectureRepository, LectureRepository>();


// Add Course Repository
builder.Services.AddScoped<IRepo<Course>, CourseRepo>();


builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));
//builder.Services.AddScoped<ILectureRepository, LectureRepository>();
builder.Services.AddScoped<IVideoService, VideoService>();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); 