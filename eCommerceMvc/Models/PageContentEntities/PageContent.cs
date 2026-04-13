namespace eCommerceMvc.Models.PageContentEntities
{
    public class PageContent
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; } = string.Empty;
        public ICollection<ContentBase> ContentBases { get; set; } = new List<ContentBase>();
    }
}
