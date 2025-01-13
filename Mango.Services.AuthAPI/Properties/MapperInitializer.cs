using AutoMapper;
using Mango.Services.AuthAPI.Model.DTO;
using Mango.Services.AuthAPI.Model;

namespace Mango.Services.AuthAPI.Properties
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<ApiUser, UserDto>().ReverseMap();

            CreateMap<ApiUser, LoginUserDto>().ReverseMap();
            CreateMap<ApiUser, CreateUserDto>().ReverseMap();

        }
    }
}
