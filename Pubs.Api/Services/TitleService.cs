using Microsoft.EntityFrameworkCore;
using Pubs.Api.Data;
using Pubs.Api.DTOs;
using Pubs.Api.Models;
using Pubs.API.Interfaces;

namespace Pubs.API.Services
{
    public class TitleService(PubsContext context) : ITitleService
    {
        private readonly PubsContext _context = context;//Campo para receber a context

        public async Task<IEnumerable<TitleDto>> GetAllTitlesAsync()
        {
            return await _context.Titles
               .Include(t => t.Publisher)
               .Select(t => new TitleDto
               {
                   TitleId = t.TitleId,
                   TitleName = t.TitleName,
                   Type = t.Type,
                   PubId = t.PubId ?? "N/A",
                   Price = t.Price ?? 0,
                   Advance = t.Advance ?? 0,
                   Royalty = t.Royalty ?? 0,
                   YtdSales = t.YtdSales ?? 0,
                   Notes = t.Notes ?? string.Empty,
                   PubDate = t.PubDate,
                   PublisherName = t.Publisher != null ? t.Publisher.PubName ?? "N/A" : "N/A" 
               })
               .ToListAsync();
        }

        public async Task DeleteTitleAsync(string id)
        {
            var title = await _context.Titles.FindAsync(id) ?? throw new ArgumentException("Titulo não encontrado");
            _context.Titles.Remove(title);
            await _context.SaveChangesAsync();
        }

        public async Task<TitleDto> GetTitleByIdAsync(string id)
        {
            var title = await _context.Titles
               .Include(t => t.Publisher)
               .FirstOrDefaultAsync(t => t.TitleId == id) ?? throw new ArgumentException("Título não encontrado");
            return new TitleDto
            {
                TitleId = title.TitleId,
                TitleName = title.TitleName,
                Type = title.Type,
                PubId = title.PubId ?? "N/A",
                Price = title.Price ?? 0,
                Advance = title.Advance ?? 0,
                Royalty = title.Royalty ?? 0,
                YtdSales = title.YtdSales ?? 0,
                Notes = title.Notes ?? string.Empty,
                PubDate = title.PubDate,
                PublisherName = title.Publisher?.PubName ?? "N/A"
            };
        }

        public async Task<TitleDto> CreateTitleAsync(CreateTitleDto titleDto)
        {
            var title = new Title
            {
                TitleId = titleDto.TitleId,
                TitleName = titleDto.TitleName,
                Type = titleDto.Type,
                PubId = titleDto.PubId,
                Price = titleDto.Price,
                Advance = titleDto.Advance,
                Royalty = titleDto.Royalty,
                Notes = titleDto.Notes,
                PubDate = DateTime.Now
            };

            _context.Titles.Add(title);
            await _context.SaveChangesAsync();

            var createdTitle = await GetTitleByIdAsync(title.TitleId) ?? throw new InvalidOperationException("Falha ao criar o Título.");
            return createdTitle;
        }

        public async Task UpdateTitleAsync(string id, CreateTitleDto titleDto)
        {
            var title = await _context.Titles.FindAsync(id) ?? throw new ArgumentException("Título não encontrado");
            title.TitleName = titleDto.TitleName;
            title.Type = titleDto.Type;
            title.PubId = titleDto.PubId;
            title.Price = titleDto.Price;
            title.Advance = titleDto.Advance;
            title.Royalty = titleDto.Royalty;
            title.Notes = titleDto.Notes;

            await _context.SaveChangesAsync();
        }
    }
}
