using App.Contracts.Models;
using AutoMapper;
using Domain.Contracts.Models;

namespace AppMapper.Profiles
{
    public class UserProfile : Profile
    {
        #region constructor

        public UserProfile()
        {
            CreateMap<User, UserRegisterModel>()
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.UserName))
                .ReverseMap()
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.UserName));
        }

        #endregion
    }
}
