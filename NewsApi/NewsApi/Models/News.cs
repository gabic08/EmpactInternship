namespace NewsApi.Models
{
    public class News
    {
        public Guid Id { get; set; }
        public string? Guid { get; set; }
        public string? Channel { get; set; }
        public string? Title { get; set; }
        public string? Creator { get; set; }
        public string? Date { get; set; }
        public string? DateReceived { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
        public string? MediaContent { get; set; }
    }
}
