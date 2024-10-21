using System;
using LearnFlow.Models;

namespace LearnFlow.Interfaces;

public interface IRepo<T> where T : class
{
  Task<IEnumerable<T>> GetAllAsync();
  Task<T> GetByIdAsync(int id);
  Task<T> CreateAsync(T entity);
  Task<T> UpdateAsync(T entity);
  Task<T> DeleteAsync(int id);
  IEnumerable<Course> SearchCourses(string searchKeyword);
}
