using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketShop.Domain.DomainModels;
using TicketShop.Domain.Identity;

namespace TicketShop.Repository
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<TicketApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Standard.ToString()));
        }

        public static async Task SeedSuperAdminAsync(UserManager<TicketApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new TicketApplicationUser
            {
                UserName = "admin@admin.com",
                NormalizedUserName = "admin@admin.com",
                Email = "admin@admin.com",
                FirstName = "Sinisha",
                LastName = "Boshkovski",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                UserShoppingCart = new ShoppingCart()
            };

            if(userManager.Users.All(x=>x.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if(user != null) {
                    await userManager.CreateAsync(defaultUser, "Test123!");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                }
            }
        }
    }
}
