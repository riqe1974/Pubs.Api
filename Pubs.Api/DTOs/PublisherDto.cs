namespace Pubs.Api.DTOs
{
    public class PublisherDto
    {
        public string PubId { get; set; } = string.Empty;
        public string PubName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int TitlesCount { get; set; }
    }

    public class CreatePublisherDto
    {
        public string PubId { get; set; } = string.Empty;
        public string PubName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }

    public class UpdatePublisherDto
    {
        public string PubName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}

// DTOs/TitleDto.cs
namespace PubsAPI.DTOs
{
    public class TitleDto
    {
        public string TitleId { get; set; } = string.Empty;
        public string TitleName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string PubId { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public decimal? Advance { get; set; }
        public int? Royalty { get; set; }
        public int? YtdSales { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime PubDate { get; set; }
        public string PublisherName { get; set; } = string.Empty;
    }

    public class CreateTitleDto
    {
        public string TitleId { get; set; } = string.Empty;
        public string TitleName { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string PubId { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public decimal? Advance { get; set; }
        public int? Royalty { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
