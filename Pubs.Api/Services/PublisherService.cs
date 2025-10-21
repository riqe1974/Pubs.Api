using Microsoft.EntityFrameworkCore;
using Pubs.Api.Data;
using Pubs.Api.DTOs;
using Pubs.Api.Models;
using Pubs.API.Interfaces;

namespace Pubs.API.Services
{
    public class PublisherService(PubsContext context) : IPublisherService
    {
        private readonly PubsContext _context = context;

        public async Task<IEnumerable<PublisherDto>> GetAllPublishersAsync()
        {
            return await _context.Publishers
                .Include(p => p.Titles)
                .Select(p => new PublisherDto
                {
                    PubId = p.PubId,
                    PubName = p.PubName ?? "N/A",
                    City = p.City ?? "N/A",
                    State = p.State ?? "N/A",
                    Country = p.Country ?? "N/A",
                    TitlesCount = p.Titles.Count
                })
                .ToListAsync();
        }

        public async Task<PublisherDto> GetPublisherByIdAsync(string id)
        {
            var publisher = await _context.Publishers
                .Include(p => p.Titles)
                .FirstOrDefaultAsync(p => p.PubId == id);

            return publisher == null
                ? throw new ArgumentException("Publicação não encontrada")
                : new PublisherDto
            {
                PubId = publisher.PubId,
                PubName = publisher.PubName ?? "N/A",
                City = publisher.City ?? "N/A",
                State = publisher.State ?? "N/A",
                Country = publisher.Country ?? "N/A",
                TitlesCount = publisher.Titles.Count
            };
        }

        public async Task<PublisherDto> CreatePublisherAsync(CreatePublisherDto publisherDto)
        {
            var publisher = new Publisher
            {
                PubId = publisherDto.PubId,
                PubName = publisherDto.PubName ?? "N/A",
                City = publisherDto.City ?? "N/A",
                State = publisherDto.State ?? "N/A",
                Country = publisherDto.Country ?? "N/A"
            };

            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();

            return new PublisherDto
            {
                PubId = publisher.PubId,
                PubName = publisher.PubName,
                City = publisher.City,
                State = publisher.State,
                Country = publisher.Country,
                TitlesCount = 0
            };
        }

        public async Task UpdatePublisherAsync(string id, UpdatePublisherDto publisherDto)
        {
            var publisher = await _context.Publishers.FindAsync(id) ?? throw new ArgumentException("Publicação não encontrada");
            publisher.PubName = publisherDto.PubName ?? "N/A";
            publisher.City = publisherDto.City ?? "N/A";
            publisher.State = publisherDto.State ?? "N/A";
            publisher.Country = publisherDto.Country ?? "N/A";

            await _context.SaveChangesAsync();
        }

        public async Task DeletePublisherAsync(string id)
        {
            var publisher = await _context.Publishers.FindAsync(id) ?? throw new ArgumentException("Publicação não encontrada");
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TitleDto>> GetPublisherTitlesAsync(string publisherId)
        {
            return await _context.Titles
                .Where(t => t.PubId == publisherId)
                .Include(t => t.Publisher)
                .Select(t => new TitleDto
                {
                    TitleId = t.TitleId,
                    TitleName = t.TitleName,
                    Type = t.Type,
                    PubId = t.PubId ?? string.Empty,
                    Price = t.Price,
                    Advance = t.Advance,
                    Royalty = t.Royalty,
                    YtdSales = t.YtdSales,
                    Notes = t.Notes ?? string.Empty,
                    PubDate = t.PubDate,
                    PublisherName = t.Publisher != null ? t.Publisher.PubName ?? "N/A" : "N/A" //Checkagem explicíta de nulo 
                })
                .ToListAsync();
        }
    }
}


