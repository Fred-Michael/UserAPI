using Users.Infrastructure.DataContext;
using Users.Infrastructure.Repository.Interfaces;
using Users.Models;

namespace Users.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDataContext _context;
        public UserRepository(UserDataContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(User userToAdd)
        {
            _context.Users.Add(userToAdd);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(string userId)
        {
            var userToRemove = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (userToRemove == null)
                throw new NullReferenceException(nameof(userToRemove));

            _context.Users.Remove(userToRemove);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User> Get(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new NullReferenceException(nameof(user));

            return user;
        }

        public async Task<bool> Update(User userToUpdate)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userToUpdate.Id);
            if (user == null)
                throw new NullReferenceException(nameof(user));

            user.FirstName = userToUpdate.FirstName;
            user.LastName = userToUpdate.LastName;
            user.EmailAddress = userToUpdate.EmailAddress;
            user.Country = userToUpdate.Country;

            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}