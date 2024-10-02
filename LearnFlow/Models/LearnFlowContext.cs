using System;
using Microsoft.EntityFrameworkCore;

namespace LearnFlow.Models;

public class LearnFlowContext : DbContext
{
  public LearnFlowContext(DbContextOptions<LearnFlowContext> options) : base(options)
  {
  }
  public DbSet<AnswerOption> AnswerOptions { get; set; }
  public DbSet<Course> Courses { get; set; }
  public DbSet<Enrollment> Enrollments { get; set; }
  public DbSet<Lecture> Lectures { get; set; }
  public DbSet<Payment> Payments { get; set; }
  public DbSet<Question> Questions { get; set; }
  public DbSet<Quiz> Quizs { get; set; }
  public DbSet<QuizResult> QuizResults { get; set; }
  public DbSet<Review> Reviews { get; set; }
  public DbSet<User> Users { get; set; }

}
