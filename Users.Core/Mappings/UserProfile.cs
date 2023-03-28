using AutoMapper;
using Users.Core.DTOs;
using Users.Models;

namespace Users.Core.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>()
                .ForMember(
                    d => d.EmailAddress,
                    s => s.MapFrom(o => o.Email))
                .ForMember(
                    d => d.Country,
                    s => s.MapFrom(o => o.UserCountry));

            CreateMap<User, UserDTO>()
                .ForMember(
                    d => d.Email,
                    s => s.MapFrom(o => o.EmailAddress))
                .ForMember(
                    d => d.UserCountry,
                    s => s.MapFrom(o => o.Country));

        }
    }
}
