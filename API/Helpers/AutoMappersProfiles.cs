using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMappersProfiles : Profile
    {
        public AutoMappersProfiles()
        {
            CreateMap<Entities.AppUser, DTOs.MemberDto>().ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url)).ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Entities.Photo, DTOs.PhotoDto>();
            //Mapping from MemberUpdateDto to AppUser
            CreateMap<DTOs.MemberUpdateDto, Entities.AppUser>();
            CreateMap<RegisterDto,AppUser>();
        }
    }
}