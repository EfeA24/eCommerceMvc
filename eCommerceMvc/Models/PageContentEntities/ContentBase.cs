namespace eCommerceMvc.Models.PageContentEntities
{
    public class ContentBase
    {
        public int Id { get; set; }
        public string Header { get; set; } = null!;

        public PageContent? PageContent { get; set; } = null;
        public int PageContentId { get; set; }

        public Images? Images { get; set; } = null;
        public int ImagesId { get; set; }
    }
}
