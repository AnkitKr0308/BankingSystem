using BankingSystem.Application.DTOs.Auth;
using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Infrastructure.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtTokenService _jwtService;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthService(UserManager<ApplicationUser> userManager, JwtTokenService jwtTokenService, RoleManager<IdentityRole> roleManager)
        {
            _userManager=userManager;
            _jwtService=jwtTokenService;
            _roleManager=roleManager;
        }
        public async Task<AuthResponseDTO> LoginAsync(LoginDTO login)
        {
            var user = await _userManager.FindByNameAsync(login.Email);
            if (user == null) 
            {
                return new AuthResponseDTO
                {
                    isSuccess = false,
                    Message = "Invalid user"
                };
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!checkPassword)
            {
                return new AuthResponseDTO
                {
                    isSuccess = false,
                    Message = "Invalid Password"
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtService.GenerateToken(user, roles);

            return new AuthResponseDTO
            {
                isSuccess = true,
                Message="User logged in successfully",
                Token = token
            };
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO register)
        {
            var existingUser = await _userManager.FindByEmailAsync(register.Email);

            if (existingUser != null)
            {
                return new AuthResponseDTO
                {
                    isSuccess = false,
                    Message="User already exists"
                };
            }

            var user = new ApplicationUser
            {
                UserName = register.Username,
                Email = register.Email,
                
            };

            

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                return new AuthResponseDTO
                {
                    isSuccess=false,
                    Message= string.Join(
                ", ",
                result.Errors.Select(e => e.Description))
                };
            }

            await _userManager.AddToRoleAsync(user, register.Role);

            return new AuthResponseDTO
            {
                isSuccess=true,
                Message="User registered successfully"
            };
        }
    }
}
