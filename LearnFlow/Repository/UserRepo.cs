using System;
using LearnFlow.Data;
using LearnFlow.Interfaces;
using LearnFlow.Models; // Ensure this namespace contains the Role enum or class
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnFlow.Repository;

public class UserRepo : IUserRepo
{
  private readonly LearnFlowContext context;

  public UserRepo(LearnFlowContext context)
  {
    this.context = context;
  }

  public Task<IActionResult> DeleteUser(string id)
  {
    throw new NotImplementedException();
  }

  public async Task<List<User>> GetAllUsers()
  {
    var users = await context.Users.ToListAsync();
    return users;
  }

  public async Task<User> GetUserById(int id)
  {
    var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
    return user;
  }

  public Task<IActionResult> LoginUser(User user)
  {
    throw new NotImplementedException();
  }

  public Task<IActionResult> RegisterUser(User user)
  {
    throw new NotImplementedException();
  }

  public Task<IActionResult> UpdateUser(User user)
  {
    throw new NotImplementedException();
  }

}
