using AutoMapper;
using SafeEntry.Core.Models.DtoModels;
using SafeEntry.Persistance.Models;

namespace SafeEntry.Core.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EventModel, EventDto>().ReverseMap();
            CreateMap<UserModel, UserModelDto>().ReverseMap();
            CreateMap<EventInvitationModel, EventInvitationDto>().ReverseMap();
        }
    }
}
