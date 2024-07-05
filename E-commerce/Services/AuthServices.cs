
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace E_commerce.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthServices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _signInManager = signInManager;
        }
        public  async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered!" };

            if (await _userManager.FindByNameAsync(model.Username) is not null)
                return new AuthModel { Message = "Username is already registered!" };

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthModel { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "User");

            var jwtSecurityToken = await CreateJwtToken(user);

            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName
            };
        }
        public async Task<AuthModel> LoginAsync(TokenRequestModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var jwtSecurityToken = await CreateJwtToken(user);
                var rolesList = await _userManager.GetRolesAsync(user);

                return new AuthModel
                {
                    IsAuthenticated = true,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Email = user.Email,
                    Username = user.UserName,
                    ExpiresOn = jwtSecurityToken.ValidTo,
                    Roles = rolesList.ToList()
                };
            }

            return new AuthModel { Message = "Invalid login attempt." };
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.Duration),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
        public async Task<IdentityResult> UpdateUserAsync(string userId, UpdateUserModel model)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ApplicationException($"User with ID '{userId}' not found.");
            }

            user.UserName = model.Username;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            var result = await _userManager.UpdateAsync(user);

            return result;
        }
        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ApplicationException($"User with ID '{userId}' not found.");
            }

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }
        public async Task<bool> AddToRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"User with ID '{userId}' not found.");
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }
        public async Task<bool> RemoveFromRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"User with ID '{userId}' not found.");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result.Succeeded;
        }
        public async Task<bool> IsInRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException($"User with ID '{userId}' not found.");
            }

            return await _userManager.IsInRoleAsync(user, roleName);
        }
    }
}
