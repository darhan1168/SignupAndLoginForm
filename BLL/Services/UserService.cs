using Core;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace BLL;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Result<bool>> AddUserAsync(AppUser user, string password)
    {
        if (user == null)
        {
            return new Result<bool>(false, $"{nameof(user)} not found");
        }
        
        var iaValidPasswordResult = ValidationManager.IsValidPassword(password);
        
        if (!iaValidPasswordResult.Item1)
        {
            return new Result<bool>(false, iaValidPasswordResult.Item2);
        }

        var createResult = await _userManager.CreateAsync(user, password);

        if (!createResult.Succeeded)
        {
            return new Result<bool>(false, string.Join(". ", createResult.Errors));
        }
        
        return new Result<bool>(true);
    }

    public async Task<Result<bool>> LoginAsync(AppUser user, string password)
    {
        if (user == null)
        {
            return new Result<bool>(false, $"{nameof(user)} not found");
        }

        var checkPassword = await _userManager.CheckPasswordAsync(user, password);

        if (!checkPassword)
        {
            return new Result<bool>(false, $"{nameof(password)} incorrect");
        }
        
        var loginResult = await _signInManager.PasswordSignInAsync(user, password, false, false);

        if (!loginResult.Succeeded)
        {
            return new Result<bool>(false, "Failed to log in");
        }
        
        return new Result<bool>(true);
    }

    public async Task<Result<bool>> LogoutAsync()
    {
        try
        {
            await _signInManager.SignOutAsync();
            
            return new Result<bool>(true);
        }
        catch (Exception ex)
        {
            return new Result<bool>(false, $"Failed to log out. Error message: {ex.Message}");
        }
    }

    public async Task<Result<AppUser>> FindUserByEmail(string email)
    {
        var appUser = await _userManager.FindByEmailAsync(email);

        if (appUser == null)
        {
            return new Result<AppUser>(false, $"Failed to find user by email : {email}");
        }

        return new Result<AppUser>(true, data: appUser);
    }
}