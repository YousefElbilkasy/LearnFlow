using System;
using LearnFlow.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlow.Interfaces;

public interface IUserRepo
{
    public Task<IActionResult> RegisterUser(User user);
    public Task<IActionResult> LoginUser(User user);
    public Task<User> GetUserById(int id);
    public Task<IActionResult> UpdateUser(User user);
    public Task<IActionResult> DeleteUser(string id);
    public Task<List<User>> GetAllUsers();
}