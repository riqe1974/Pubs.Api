using Pubs.Api.DTOs;

namespace Pubs.API.Interfaces
{
    public interface IPublisherService
    {
        Task<IEnumerable<PublisherDto>> GetAllPublishersAsync();
        Task<PublisherDto> GetPublisherByIdAsync(string id);
        Task<PublisherDto> CreatePublisherAsync(CreatePublisherDto publisherDto);
        Task UpdatePublisherAsync(string id, UpdatePublisherDto publisherDto);
        Task DeletePublisherAsync(string id);
        Task<IEnumerable<TitleDto>> GetPublisherTitlesAsync(string publisherId);
    }
}

