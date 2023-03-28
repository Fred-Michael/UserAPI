using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Users.Core.DTOs;
using Users.Infrastructure.Repository.Interfaces;
using Users.Models;

namespace Users.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }
        //public async Task<bool> RegisterUser(UserDTO userToAdd)
        //{
        //    //this is where jwt comes into play
        //    if (userToAdd == null)
        //    {
        //        throw new ArgumentNullException(nameof(userToAdd));
        //    }

        //    var user = _mapper.Map<User>(userToAdd);
        //    var token = await _token.GenerateTokenAsync(user);
        //    user.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);

        //    return await _userRepo.Add(user);
        //}
        public async Task<UserDTO?> CheckUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            var user = await _userRepo.Get(userId);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> UpdateUser(UserDTO userToUpdate)
        {
            if (userToUpdate == null)
                throw new ArgumentNullException(nameof(userToUpdate));

            var updatedUser = await _userRepo.Update(_mapper.Map<User>(userToUpdate));
            if (!updatedUser)
                throw new Exception();

            return true;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            var deletedUser = await _userRepo.Delete(userId);
            if (!deletedUser)
                throw new Exception();

            return true;
        }
    }
}
