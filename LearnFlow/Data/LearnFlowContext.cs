using System;
using LearnFlow.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace LearnFlow.Data;

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
  public DbSet<Progress> Progresses { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    // Configure cascade delete for QuizResults when a Quiz is deleted
    modelBuilder.Entity<QuizResult>()
        .HasOne(qr => qr.Quiz)
        .WithMany() // No navigation property on the Quiz side
        .HasForeignKey(qr => qr.QuizId)
        .OnDelete(DeleteBehavior.Cascade); // Cascade delete

    // Configure composite key for Progress
    modelBuilder.Entity<Progress>()
        .HasKey(p => new { p.EnrollmentId, p.LectureId });
        
    modelBuilder.Entity<Quiz>()
           .HasMany(q => q.Questions)
           .WithOne(q => q.Quiz)
           .HasForeignKey(q => q.QuizId)
           .OnDelete(DeleteBehavior.Cascade); // Cascade delete for questions

        modelBuilder.Entity<Question>()
            .HasMany(q => q.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
  }

}
