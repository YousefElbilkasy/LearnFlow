using CloudinaryDotNet;
using LearnFlow.Data;
using LearnFlow.Interfaces;
using LearnFlow.Models;
using LearnFlow.Repositories;
using LearnFlow.Repository;
using LearnFlow.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LearnFlow.Helpers;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add database context
builder.Services.AddDbContext<LearnFlowContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// Add Identity
builder.Services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<LearnFlowContext>()
                .AddDefaultTokenProviders();

// Add Repositories
builder.Services.AddScoped<IRepo<Course>, CourseRepo>();
builder.Services.AddScoped<ILectureRepository, LectureRepository>();
builder.Services.AddScoped<CourseRepo, CourseRepo>();
builder.Services.AddScoped<IRepo<Quiz>, QuizRepo>();
builder.Services.AddScoped<IRepo<Course>, CourseRepo>();
builder.Services.AddScoped<IRepo<Question>, QuestionRepo>();
builder.Services.AddScoped<IRepo<Lecture>, LectureRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IVideoService, VideoService>();
builder.Services.AddScoped<IEnrollmentRepo, EnrollmentRepo>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IStudentRepo, StudentRepo>();


// Configure Cloudinary
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

// Register Cloudinary service
builder.Services.AddSingleton(sp =>
{
  var config = sp.GetRequiredService<IOptions<CloudinarySettings>>().Value;
  var account = new Account(config.CloudName, config.ApiKey, config.ApiSecret);
  return new Cloudinary(account);
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
