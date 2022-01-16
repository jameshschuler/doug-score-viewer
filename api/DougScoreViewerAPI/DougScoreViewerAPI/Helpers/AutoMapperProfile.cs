using AutoMapper;
using DougScoreViewerAPI.Entities;
using DougScoreViewerAPI.Models;

namespace DougScoreViewerAPI.Helpers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile() 
    {   
        /*CreateMap<DougScores, Test>()
            .ForMember(e => e.Id, opt => opt.MapFrom(e => e.Id))
            .ForMember(e => e.FilmLocation, opt => opt.MapFrom(e => e.Id.ToString()));*/
    }
}