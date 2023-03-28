using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Users.Infrastructure.DataContext;
using Users.Models;

namespace Users.Infrastructure
{
    public class Preseeder
    {
        public static async Task Seeder(UserDataContext ctx,
                                RoleManager<IdentityRole> rolemanager,
                                UserManager<User> userManager)
        {
            //check that db has been properly created
            ctx.Database.EnsureCreated();

            //if there are no existing roles, add new roles
            if (!rolemanager.Roles.Any())
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole(UserRoles.Admin),
                    new IdentityRole(UserRoles.User)
                };
                foreach (var role in roles)
                {
                    await rolemanager.CreateAsync(role);
                }
            }

            if (!userManager.Users.Any())
            {
                var adminJsonDetails = File.ReadAllText("SeedData.json");
                var adminDetails = JsonConvert.DeserializeObject<User>(adminJsonDetails);
                var admin = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = adminDetails?.FirstName,
                    LastName = adminDetails?.LastName,
                    EmailAddress = adminDetails?.EmailAddress,
                    Email = adminDetails?.Email,
                    Country = adminDetails?.Country
                };

                var result = await userManager.CreateAsync(admin, "Adm1nP@$$word");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, UserRoles.Admin);
                }
            }
        }
    }
}
