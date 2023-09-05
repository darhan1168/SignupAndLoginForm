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
