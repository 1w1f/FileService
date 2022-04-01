using AutoMapper;
using DataModel.User;
using DataModel.User.Vo;

namespace FileService.AutoMapper.Profiles;

public class UserProfile:Profile
{

    public UserProfile()
    {
        CreateMap<UserDto,UserVo>().ForMember(des=>des.Name,option=>option.MapFrom(Dto=>Dto.Name));
        
        CreateMap<UserVo,UserDto>().ForMember(des=>des.Name,option=>option.MapFrom(vo=>vo.Name)).ForMember(des=>des.PassWord,option=>option.MapFrom(vo=>vo.PassWord));
    }
}
