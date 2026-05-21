using BankingSystem.Application.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Infrastructure.Authentication
{
    public class IdentityRoleSeeder
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in AppRoles.roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
