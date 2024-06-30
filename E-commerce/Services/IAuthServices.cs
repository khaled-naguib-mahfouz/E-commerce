using Microsoft.AspNetCore.Identity;

namespace E_commerce.Services
{
    public interface IAuthServices
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(TokenRequestModel model);
        Task LogoutAsync();
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<IdentityResult> UpdateUserAsync(string userId, UpdateUserModel model);
        Task<bool> DeleteUserAsync(string userId);
        Task<bool> IsInRoleAsync(string userId, string roleName);
        Task<bool> RemoveFromRoleAsync(string userId, string roleName);
        Task<bool> AddToRoleAsync(string userId, string roleName);
    }
}
