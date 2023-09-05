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
    }
}