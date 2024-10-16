using CloudinaryDotNet;
using LearnFlow.Interfaces;
using LearnFlow.Models;
using LearnFlow.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LearnFlowContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

// Add Identity
builder.Services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<LearnFlowContext>()
                .AddDefaultTokenProviders();

// Add Repositories
builder.Services.AddScoped<CourseRepo, CourseRepo>();
builder.Services.AddScoped<IRepo<Quiz>, QuizRepo>();
builder.Services.AddScoped<IRepo<Question>, QuestionRepo>();
builder.Services.AddScoped<IRepo<Lecture>, LectureRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<UploadToCloudinaryRepo, UploadToCloudinaryRepo>();

// Configure Cloudinary
var cloudinarySettings = builder.Configuration.GetSection("Cloudinary").Get<CloudinarySettings>();
var cloudinary = new Cloudinary(new Account(
    cloudinarySettings.CloudName,
    cloudinarySettings.ApiKey,
    cloudinarySettings.ApiSecret
));
builder.Services.AddSingleton(cloudinary);

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