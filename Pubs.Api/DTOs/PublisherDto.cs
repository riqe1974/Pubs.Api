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


