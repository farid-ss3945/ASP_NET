using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApplication5.Data;
using WebApplication5.DTOs;
using WebApplication5.DTOs.User;
using WebApplication5.Migrations;
using WebApplication5.Models;
using WebApplication5.Services.Interfaces;

namespace WebApplication5.Services
{
    public class UserService : IUserService
    {
        private readonly InvoiceManagerDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
        public UserService(InvoiceManagerDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _config = configuration;
        }

        public async Task<bool> ChangePasswordAsync(int id,ChangePasswordDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == id);
            if (user == null)
            {
                return false;
            }
            if (HashPassword(dto.CurrentPassword) != user.Password)
            {
                return false;
            }
            user.Password = HashPassword(dto.NewPassword);
            user.UpdatedAt = DateTimeOffset.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Email == dto.Email);
            if (user == null) {
                return null;
            }
            if (HashPassword(dto.Password) != user.Password) {
                return null;
            }
            var token = GenerateToken(user);
            return new AuthResponseDto
            {
                Token = token,
                User = _mapper.Map<UserResponseDto>(user)
            };
        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
        {
            var checkUser = await _context.Users.FirstOrDefaultAsync(i => i.Email == dto.Email);
            if (checkUser != null)
            {
                return null;
            }
            var user = _mapper.Map<User>(dto);
            user.CreatedAt = DateTimeOffset.UtcNow;
            user.Password=HashPassword(dto.Password);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var token = GenerateToken(user);
            return new AuthResponseDto { 
                Token = token,
                User = _mapper.Map<UserResponseDto>(user)
            };

        }

        public async Task<UserResponseDto?> UpdateProfileAsync(int id,UpdateProfileDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == id);
            if (user == null) return null;

            user.Name = dto.Name ?? user.Name;
            user.Address = dto.Address ?? user.Address;
            user.PhoneNumber = dto.PhoneNumber ?? user.PhoneNumber;

            user.UpdatedAt = DateTimeOffset.UtcNow;
            await _context.SaveChangesAsync();

            return _mapper.Map<UserResponseDto>(user);
        }
        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
        );
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7), 
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
