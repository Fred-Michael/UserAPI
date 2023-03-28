using Users.Core.DTOs;

namespace Users.Core.Services
{
    public interface IUserService
    {
        Task<UserDTO?> CheckUser(string userId);
        Task<bool> UpdateUser(UserDTO userToUpdate);
        Task<bool> DeleteUser(string userId);
    }
}
