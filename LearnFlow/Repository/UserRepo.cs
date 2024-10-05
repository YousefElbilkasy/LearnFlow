using System;
using LearnFlow.Interfaces;
using LearnFlow.Models;
using Microsoft.AspNetCore.Mvc;

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

  public List<User> GetAllUsers()
  {
    var users = context.Users.ToList();
    return users;
  }

  public Task<IActionResult> GetUser(string id)
  {
    throw new NotImplementedException();
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
