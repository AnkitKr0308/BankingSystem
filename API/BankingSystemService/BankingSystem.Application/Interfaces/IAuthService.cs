using BankingSystem.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO register);
        Task<AuthResponseDTO> LoginAsync(LoginDTO login);
        Task<AuthResponseDTO> RefreshTokenAsync(string refreshToken);
    }
}
