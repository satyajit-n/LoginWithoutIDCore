using AutoMapper;
using LoginWithoutIDCore.Models.Domain;
using LoginWithoutIDCore.Models.Dto;

namespace LoginWithoutIDCore.Mapper
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<AddUserDto,User>().ReverseMap();
        }
    }
}
