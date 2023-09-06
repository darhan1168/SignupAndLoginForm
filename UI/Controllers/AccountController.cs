using BLL;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using UI.Models;

namespace UI.Controllers;

public class AccountController : Controller
{
    private IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> SignUp(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var appUser = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.FirstName + model.LastName
            };

            var createResult = await _userService.AddUserAsync(appUser, model.Password);

            if (!createResult.IsSuccessful)
            {
                TempData["ErrorMessage"] = createResult.Message;
                
                return View(model);
            }

            return RedirectToAction("LogIn", "Account");
        }
        
        return View(model);
    }
    
    [HttpGet]
    public IActionResult LogIn()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LogIn(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var findAppUserResult = await _userService.FindUserByEmail(model.Email);

            if (!findAppUserResult.IsSuccessful)
            {
                TempData["ErrorMessage"] = findAppUserResult.Message;
                
                return View(model);
            }

            var loginResult = await _userService.LoginAsync(findAppUserResult.Data, model.Password);
            
            if (!loginResult.IsSuccessful)
            {
                TempData["ErrorMessage"] = loginResult.Message;
                
                return View(model);
            }
            
            return RedirectToAction("Privacy", "Home");
        }
        
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> LogOut()
    {
        var logoutResult = await _userService.LogoutAsync();

        if (!logoutResult.IsSuccessful)
        {
            TempData["ErrorMessage"] = logoutResult.Message;
                
            return RedirectToAction("Privacy", "Home");
        }
        
        return RedirectToAction("LogIn", "Account");
    }
}