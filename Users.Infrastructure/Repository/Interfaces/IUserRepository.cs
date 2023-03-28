using Users.Models;

namespace Users.Infrastructure.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> Add(User userToAdd);
        Task<User> Get(string userId);
        Task<bool> Update(User userToUpdate);
        Task<bool> Delete(string userId);
    }
}