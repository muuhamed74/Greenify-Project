using AutoMapper;
using Agricultural.Core.Models;
using Agricultural.DTOs;
using Agricultural.Helpers;
using Agricultural.Core.DTOs;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PlantsInfo, PlantInfoDTO>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.PlantImages));

        CreateMap<PlantImages, PlantImageDTO>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<ImageUrlResolver>());

        // Add mapping for PlantDetails
        CreateMap<Agricultural.Core.Models.PlantDetails, Agricultural.DTOs.PlantDetails>();

        CreateMap<PlantAdditionalData, HealthyPlantResponseDto>();

        CreateMap<PlantAdditionalData, PlantResponseDto>();

    }
}