using Pubs.Api.DTOs;

namespace Pubs.API.Interfaces
{
    public interface ITitleService
    {
        Task<IEnumerable<TitleDto>> GetAllTitlesAsync();
        Task<TitleDto> GetTitleByIdAsync(string id);
        Task<TitleDto> CreateTitleAsync(CreateTitleDto titleDto);
        Task UpdateTitleAsync(string id, CreateTitleDto titleDto);
        Task DeleteTitleAsync(string id);
    }
}