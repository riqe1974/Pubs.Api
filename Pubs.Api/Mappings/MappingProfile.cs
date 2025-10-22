using AutoMapper;
using Pubs.Api.DTOs;
using Pubs.Api.Models;

namespace Pubs.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeamento Publisher
            CreateMap<Publisher, PublisherDto>();
            CreateMap<CreatePublisherDto, Publisher>();
            CreateMap<UpdatePublisherDto, Publisher>();

            // Mapeamento Title
            CreateMap<Title, TitleDto>();
            CreateMap<CreateTitleDto, Title>();
        }
    }
}