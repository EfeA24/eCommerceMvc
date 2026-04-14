namespace eCommerceMvc.Models.PageContentEntities
{
    public enum PageStatus
    {
        Draft = 0,
        Published = 1
    }

    public class PageContent
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string LayoutJson { get; set; } = "{}";
        public PageStatus Status { get; set; } = PageStatus.Draft;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
