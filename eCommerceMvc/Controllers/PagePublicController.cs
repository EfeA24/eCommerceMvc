using eCommerceMvc.Models.PageContentEntities;
using eCommerceMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceMvc.Controllers
{
    [Route("pages")]
    public class PagePublicController : Controller
    {
        private readonly IPageContentService _pageContentService;
        private readonly PageRenderService _pageRenderService;

        public PagePublicController(IPageContentService pageContentService, PageRenderService pageRenderService)
        {
            _pageContentService = pageContentService;
            _pageRenderService = pageRenderService;
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            var page = await _pageContentService.GetPublishedBySlugAsync(slug);
            if (page is null)
            {
                return NotFound();
            }

            var html = _pageRenderService.RenderFromJson(page.LayoutJson);
            return View("~/Views/Page/Details.cshtml", new PageRenderViewModel
            {
                Slug = page.Slug,
                Title = page.Title,
                RenderedHtml = html,
                IsPreview = false
            });
        }
    }
}
