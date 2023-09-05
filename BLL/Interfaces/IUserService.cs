using Core.Models;

namespace BLL;

public interface IUserService
{
    Task<Result<bool>> AddUserAsync(AppUser user, string password);
    Task<Result<bool>> LoginAsync(AppUser user, string password);
    Task<Result<AppUser>> FindUserByEmail(string email);
}