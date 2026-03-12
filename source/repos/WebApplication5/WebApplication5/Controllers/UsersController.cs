using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.DTOs;
using WebApplication5.DTOs.User;
using WebApplication5.Services.Interfaces;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) { 
            _userService = userService;
        }
        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Register(RegisterDto dto)
        {
            var user=await _userService.RegisterAsync(dto);
            if (user != null) { 
                return Ok(user);
            }
            return BadRequest("Email already in use");
        }
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
        {
            var user = await _userService.LoginAsync(dto);
            if (user == null)
                return Unauthorized("Invalid email or password");
            return Ok(user);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponseDto>> Update(int id, UpdateProfileDto dto)
        {
            var user = await _userService.UpdateProfileAsync(id, dto);
            if (user == null)
                return NotFound("User not found");
            return Ok(user);
        }

        [Authorize]
        [HttpPost("{id}/change-password")]
        public async Task<ActionResult> ChangePassword(int id, ChangePasswordDto dto)
        {
            var user = await _userService.ChangePasswordAsync(id, dto);
            if (user == false)
                return BadRequest("Current password is incorrect or user not found");
            return Ok("Password changed successfully");
        }
    }
}
