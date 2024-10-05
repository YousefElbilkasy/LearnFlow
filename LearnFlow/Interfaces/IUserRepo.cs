using System;
using LearnFlow.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Interfaces;

public interface IUserRepo
{
  public Task<IActionResult> RegisterUser(User user);
  public Task<IActionResult> LoginUser(User user);
  public Task<IActionResult> GetUser(string id);
  public Task<IActionResult> UpdateUser(User user);
  public Task<IActionResult> DeleteUser(string id);
  public List<User> GetAllUsers();
}
