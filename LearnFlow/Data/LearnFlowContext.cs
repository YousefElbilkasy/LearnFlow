using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace LearnFlow.Models;

public class LearnFlowContext : IdentityDbContext<User, IdentityRole<int>, int>
{
  public LearnFlowContext(DbContextOptions<LearnFlowContext> options) : base(options)
  {
  }
  public DbSet<Answer> Answers { get; set; }
  public DbSet<Course> Courses { get; set; }
  public DbSet<Enrollment> Enrollments { get; set; }
  public DbSet<Lecture> Lectures { get; set; }
  public DbSet<Payment> Payments { get; set; }
  public DbSet<Question> Questions { get; set; }
  public DbSet<Quiz> Quizs { get; set; }
  public DbSet<QuizResult> QuizResults { get; set; }
  public DbSet<Review> Reviews { get; set; }
}
