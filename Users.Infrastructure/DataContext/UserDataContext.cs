using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Users.Models;

namespace Users.Infrastructure.DataContext
{
    public class UserDataContext : IdentityDbContext<User>
    {
        public UserDataContext(DbContextOptions<UserDataContext> options) : base(options) { }
    }
}