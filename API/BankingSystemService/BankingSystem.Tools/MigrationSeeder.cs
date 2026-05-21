using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingSystem.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BankingSystem.Tools
{
    public class MigrationSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public MigrationSeeder(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task SeedRolesAsync()
        {
            await IdentityRoleSeeder.SeedAsync(_roleManager);
        }
    }
}
