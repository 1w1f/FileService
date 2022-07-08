using AutoMapper;
using DataModel.User;
using DataModel.User.Vo;

namespace FileService.AutoMapper.Profiles;

public class UserProfile : Profile
{

    public UserProfile()
    {
        CreateMap<UserDto, UserVo>().ForMember(des => des.Name, option => option.MapFrom(Dto => Dto.Name)).ForMember(des => des.Id, option => option.MapFrom(source => source.Id));
        // .ForMember(des => des.PassWord, option => option.MapFrom(source => source.PassWord))

        CreateMap<UserVo, UserDto>().ForMember(des => des.Name, option => option.MapFrom(vo => vo.Name)).ForMember(des => des.PassWord, option => option.MapFrom(vo => vo.PassWord));
    }
}
