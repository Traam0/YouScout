using AutoMapper;
using YouScout.Application.Common.Models;
using YouScout.Domain.Entities;

namespace YouScout.Application.Common.Profiles;

public class UserProfiles : Profile
{
    public UserProfiles()
    {
        CreateMap<User, ProfileDto>()
            .ForMember(destination => destination.Name, options =>
                options.MapFrom(source => source.FullName))
            .ForMember(destination => destination.Avatar, options =>
                options.MapFrom(source => source.ProfilePictureUrl))
            .ForMember(destination => destination.Posts, options =>
                options.MapFrom(source => 0)) // TODO 
            .ForMember(destination => destination.Following, options =>
                options.MapFrom(source => source.Following.Count()))
            .ForMember(destination => destination.Followers, options =>
                options.MapFrom(source => source.Followers.Count()));
    }
}