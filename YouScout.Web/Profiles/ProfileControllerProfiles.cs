using AutoMapper;
using YouScout.Application.Users.Commands;
using YouScout.Web.Models;

namespace YouScout.Web.Profiles;

public class ProfileControllerProfiles : Profile
{
    public ProfileControllerProfiles()
    {
        CreateMap<CreateProfileRequest, CreateCurrentUserProfileCommand>();
    }
}