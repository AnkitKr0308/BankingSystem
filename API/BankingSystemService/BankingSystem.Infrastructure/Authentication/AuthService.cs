using Azure;
using BankingSystem.Application.DTOs.Auth;
using BankingSystem.Application.Interfaces;
using BankingSystem.Domain.Entity.Authentication;
using BankingSystem.Infrastructure.Data.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly BankingDbContext _dbContext;
        public AuthService(UserManager<ApplicationUser> userManager, 
            JwtTokenService jwtTokenService, 
            RoleManager<IdentityRole> roleManager,
            BankingDbContext dbContext)

        {
            _userManager=userManager;
            _jwtService=jwtTokenService;
            _roleManager=roleManager;
            _dbContext=dbContext;
        }
        public async Task<AuthResponseDTO> LoginAsync(LoginDTO login)
        {
            var user = await _userManager.FindByNameAsync(login.Username) ?? await _userManager.FindByEmailAsync(login.Username);
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

            var refreshToken = _jwtService.GenerateRefreshToken();

            await _dbContext.RefreshTokens.AddAsync(new RefreshToken
            {
                Token=refreshToken,
                Expires=DateTime.UtcNow.AddHours(1),
                isRevoked=false,
                UserId=user.Id
            });

            await _dbContext.SaveChangesAsync();

            return new AuthResponseDTO
            {
                isSuccess = true,
                Message="User logged in successfully",
                Token = token,
                RefreshToken = refreshToken
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

            var existingRole = await _roleManager.FindByNameAsync(register.Role);
            if (existingRole == null)
            {
                return new AuthResponseDTO
                {
                    isSuccess = false,
                    Message = "Role doesn't exists"
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

        public async Task<AuthResponseDTO> RefreshTokenAsync(string refreshToken)
        {
            var storedToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);

            if (storedToken == null || storedToken.isRevoked || storedToken.Expires < DateTime.UtcNow)
                return null;

            var user = await _userManager.FindByIdAsync(storedToken.UserId);

            var roles = await _userManager.GetRolesAsync(user);

            var newToken =  _jwtService.GenerateToken(user, roles);

            return new AuthResponseDTO
            {
                Token = newToken
            };

        }
    }
}
