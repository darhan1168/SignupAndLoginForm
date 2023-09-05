using Core.Models;

namespace BLL;

public interface IUserService
{
    Task<Result<bool>> AddUserAsync(AppUser user, string password);
}