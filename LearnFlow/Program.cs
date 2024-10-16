using LearnFlow.Data;
using CloudinaryDotNet;
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
// Add Identity
builder.Services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<LearnFlowContext>()
                .AddDefaultTokenProviders();

// Add Repositories
builder.Services.AddScoped<ILectureRepository, LectureRepository>();
builder.Services.AddScoped<CourseRepo, CourseRepo>();
builder.Services.AddScoped<IRepo<Quiz>, QuizRepo>();
builder.Services.AddScoped<IRepo<Question>, QuestionRepo>();
builder.Services.AddScoped<IRepo<Lecture>, LectureRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<UploadToCloudinaryRepo, UploadToCloudinaryRepo>();
builder.Services.AddScoped<IVideoService, VideoService>();
//builder.Services.AddScoped<ILectureRepository, LectureRepository>();

// Configure Cloudinary
builder.Services.AddSingleton(cloudinary);
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));



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