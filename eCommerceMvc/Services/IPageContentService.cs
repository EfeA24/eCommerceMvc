using eCommerceMvc.Models.PageContentEntities;

namespace eCommerceMvc.Services
{
    public interface IPageContentService
    {
        Task<List<PageBuilderListItemViewModel>> GetListAsync();
        Task<PageContent?> GetByIdAsync(int id);
        Task<PageContent?> GetPublishedBySlugAsync(string slug);
        Task<bool> SlugExistsAsync(string slug, int? excludingId = null);
        bool IsValidLayoutJson(string layoutJson);
        Task<PageContent> SaveAsync(PageBuilderFormViewModel model);
        string GenerateSlug(string title);
    }
}
