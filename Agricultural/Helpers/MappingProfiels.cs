using System.Runtime;
using Agricultural.Core.Models;
using AutoMapper;
using Agricultural.DTOs;

namespace Agricultural.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()    
        {
            CreateMap<PlantsInfo, PlantInfoDTO>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.PlantImages));

            CreateMap<PlantImages, PlantImageDTO>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<ImageUrlResolver>());

        }
    }

}

