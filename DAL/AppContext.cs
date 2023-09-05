using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class AppContext : IdentityDbContext<AppUser>
{
    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
    }
}