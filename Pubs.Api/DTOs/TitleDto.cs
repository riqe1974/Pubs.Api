namespace Pubs.Api.DTOs
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
