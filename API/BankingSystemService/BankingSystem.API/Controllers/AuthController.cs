using BankingSystem.Application.DTOs.Auth;
using BankingSystem.Application.Interfaces;
using BankingSystem.Application.Services;
using BankingSystem.Domain.Entity;
using BankingSystem.Domain.Entity.Authentication;
using BankingSystem.Infrastructure.Data.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BankingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly BankingDbContext _bankingContext;
        
        public AuthController(IAuthService authService, RoleManager<IdentityRole> roleManager, BankingDbContext bankingDbContext)
        {
            _authService = authService;
            _roleManager = roleManager;
            _bankingContext = bankingDbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAccount(RegisterDTO registerDTO)
        {
            var result = await _authService.RegisterAsync(registerDTO);

            if (!result.isSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser (LoginDTO loginDTO)
        {
            var result = await _authService.LoginAsync(loginDTO);
            if (!result.isSuccess)
            {
                return Unauthorized(result);
            }

            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            });

            return Ok(new
            {
                result.isSuccess,
                result.Token,
                result.Message
            });
        }

        [HttpGet("roles")]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles
                .OrderBy(r => r.Name)
                .Select(r => r.Name)
                .ToList();

            return Ok(roles);
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized("No refresh token");

           var result = await _authService.RefreshTokenAsync(refreshToken);
            if (result == null)
            {
                return Unauthorized("Invalid Refresh Token");
            }

            return Ok(result);
        }
    }
}
