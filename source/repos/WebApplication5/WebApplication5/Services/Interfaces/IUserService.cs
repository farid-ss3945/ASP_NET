using System.Security.Cryptography;
using System.Text;
using WebApplication5.DTOs;
using WebApplication5.DTOs.User;
using WebApplication5.Models;

namespace WebApplication5.Services.Interfaces
{
    public interface IUserService
    {
        Task<AuthResponseDto?> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
        Task<UserResponseDto?> UpdateProfileAsync(int id,UpdateProfileDto dto);
        Task<bool> ChangePasswordAsync(int id,ChangePasswordDto dto);
        string GenerateToken(User user);
        string HashPassword(string password);
    }
}
