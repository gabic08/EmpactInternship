namespace NewsApi.Models
{
    public class DuplicatedNews
    {
        public Guid Id { get; set; }
        public string? GuidLink { get; set; }
        public string? DateReceived { get; set; }
        public string? DateInitialNewsReceived { get; set; }
        public string? NewsSource { get; set; }


        public DuplicatedNews(Guid Id, string? GuidLink, string? DateReceived, string? DateInitialNewsReceived, string? NewsSource)
        {
            this.Id = Id;
            this.GuidLink = GuidLink;
            this.DateReceived = DateReceived;
            this.DateInitialNewsReceived = DateInitialNewsReceived;
            this.NewsSource = NewsSource;
        }
    }
}
