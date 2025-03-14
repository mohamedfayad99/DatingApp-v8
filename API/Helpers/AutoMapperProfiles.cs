using API.Dtos;
using API.Entities;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles:Profile
{
     public AutoMapperProfiles()
    {
        CreateMap<AppUser,MemberDTO>()
            .ForMember(dist=>dist.PhotoUrl,o=>o.MapFrom(s=>s.Photos.FirstOrDefault(x=>x.IsMain)!.Url));
       CreateMap<Photo,PhotoDTO>();
        CreateMap<MemberUpdatesDTO,AppUser>();
    }  

}
