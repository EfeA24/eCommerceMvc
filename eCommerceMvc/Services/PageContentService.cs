using System.Text.Json;
using eCommerceMvc.Context;
using eCommerceMvc.Models.PageContentEntities;
using Microsoft.EntityFrameworkCore;

namespace eCommerceMvc.Services
{
    public class PageContentService : IPageContentService
    {
        private readonly AppDbContext _dbContext;

        public PageContentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<PageBuilderListItemViewModel>> GetListAsync()
        {
            return _dbContext.pages
                .OrderByDescending(x => x.UpdatedAt)
                .Select(x => new PageBuilderListItemViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Slug = x.Slug,
                    Status = x.Status,
                    UpdatedAt = x.UpdatedAt
                })
                .ToListAsync();
        }

        public Task<PageContent?> GetByIdAsync(int id)
        {
            return _dbContext.pages.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<PageContent?> GetPublishedBySlugAsync(string slug)
        {
            return _dbContext.pages.FirstOrDefaultAsync(x => x.Slug == slug && x.Status == PageStatus.Published);
        }

        public Task<bool> SlugExistsAsync(string slug, int? excludingId = null)
        {
            return _dbContext.pages.AnyAsync(x => x.Slug == slug && x.Id != excludingId);
        }

        public bool IsValidLayoutJson(string layoutJson)
        {
            try
            {
                _ = JsonSerializer.Deserialize<PageLayoutDto>(layoutJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<PageContent> SaveAsync(PageBuilderFormViewModel model)
        {
            PageContent page;
            if (model.Id.HasValue)
            {
                page = await _dbContext.pages.FirstAsync(x => x.Id == model.Id.Value);
            }
            else
            {
                page = new PageContent
                {
                    CreatedAt = DateTime.UtcNow
                };
                _dbContext.pages.Add(page);
            }

            page.Title = model.Title.Trim();
            page.Slug = model.Slug.Trim().ToLowerInvariant();
            page.LayoutJson = model.LayoutJson;
            page.Status = model.Status;
            page.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return page;
        }

        public string GenerateSlug(string title)
        {
            var normalized = (title ?? string.Empty).Trim().ToLowerInvariant();
            var chars = normalized
                .Select(ch => char.IsLetterOrDigit(ch) ? ch : '-')
                .ToArray();
            var raw = new string(chars);
            while (raw.Contains("--", StringComparison.Ordinal))
            {
                raw = raw.Replace("--", "-", StringComparison.Ordinal);
            }

            return raw.Trim('-');
        }
    }
}
