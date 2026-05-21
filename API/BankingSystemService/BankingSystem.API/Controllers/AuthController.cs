using BankingSystem.Application.DTOs.Auth;
using BankingSystem.Domain.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BankingSystem.Application.Services;
using BankingSystem.Application.Interfaces;
using Microsoft.Identity.Client;

namespace BankingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public AuthController(IAuthService authService, RoleManager<IdentityRole> roleManager)
        {
            _authService = authService;
            _roleManager = roleManager;
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
            return Ok(result);
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
    }
}
