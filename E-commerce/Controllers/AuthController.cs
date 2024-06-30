using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
            private readonly IAuthServices _authServices;

            public AuthController(IAuthServices authServices)
            {
                _authServices = authServices;
            }

            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] RegisterModel model)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _authServices.RegisterAsync(model);

                if (result.Token == null)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] TokenRequestModel model)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _authServices.LoginAsync(model);

                if (result.Token == null)
                {
                    return Unauthorized(result);
                }

                return Ok(result);
            }
        [Authorize]

            [HttpPost("logout")]
            public async Task<IActionResult> Logout()
            {
                await _authServices.LogoutAsync();
                return Ok(new { message = "Logout successful." });
            }

            [HttpPut("update/{userId}")]
            public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserModel model)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _authServices.UpdateUserAsync(userId, model);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return Ok(new { message = "User updated successfully." });
            }

            [HttpDelete("delete/{userId}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteUser(string userId)
            {
                var result = await _authServices.DeleteUserAsync(userId);

                if (!result)
                {
                    return NotFound(new { message = $"User with ID '{userId}' not found." });
                }

                return Ok(new { message = "User deleted successfully." });
            }

            [HttpPost("add-to-role/{userId}/{roleName}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AddToRole(string userId, string roleName)
            {
                var result = await _authServices.AddToRoleAsync(userId, roleName);

                if (!result)
                {
                    return BadRequest(new { message = $"Failed to add user to role '{roleName}'." });
                }

                return Ok(new { message = $"User added to role '{roleName}' successfully." });
            }

            [HttpPost("remove-from-role/{userId}/{roleName}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> RemoveFromRole(string userId, string roleName)
            {
                var result = await _authServices.RemoveFromRoleAsync(userId, roleName);

                if (!result)
                {
                    return BadRequest(new { message = $"Failed to remove user from role '{roleName}'." });
                }

                return Ok(new { message = $"User removed from role '{roleName}' successfully." });
            }

            [HttpGet("is-in-role/{userId}/{roleName}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> IsInRole(string userId, string roleName)
            {
                var result = await _authServices.IsInRoleAsync(userId, roleName);

                return Ok(new { isInRole = result });
            }
        }
    }

