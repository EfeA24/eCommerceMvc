using eCommerceMvc.Models.PageContentEntities;
using eCommerceMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/page-builder")]
    public class PageBuilderController : Controller
    {
        private readonly IPageContentService _pageContentService;
        private readonly PageRenderService _pageRenderService;

        public PageBuilderController(IPageContentService pageContentService, PageRenderService pageRenderService)
        {
            _pageContentService = pageContentService;
            _pageRenderService = pageRenderService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var pages = await _pageContentService.GetListAsync();
            return View("~/Views/Page/Index.cshtml", new PageBuilderIndexViewModel { Pages = pages });
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View("~/Views/Page/Builder.cshtml", new PageBuilderFormViewModel());
        }

        [HttpGet("{id:int}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var page = await _pageContentService.GetByIdAsync(id);
            if (page is null) return NotFound();

            return View("~/Views/Page/Builder.cshtml", new PageBuilderFormViewModel
            {
                Id = page.Id,
                Title = page.Title,
                Slug = page.Slug,
                LayoutJson = page.LayoutJson,
                Status = page.Status
            });
        }

        [HttpPost("save")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(PageBuilderFormViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Title))
                ModelState.AddModelError(nameof(model.Title), "Title is required.");
            if (string.IsNullOrWhiteSpace(model.Slug))
                model.Slug = _pageContentService.GenerateSlug(model.Title);
            if (await _pageContentService.SlugExistsAsync(model.Slug, model.Id))
                ModelState.AddModelError(nameof(model.Slug), "Slug already exists.");
            if (!_pageContentService.IsValidLayoutJson(model.LayoutJson))
                ModelState.AddModelError(nameof(model.LayoutJson), "Layout JSON is invalid.");
            if (!ModelState.IsValid)
                return View("~/Views/Page/Builder.cshtml", model);

            var page = await _pageContentService.SaveAsync(model);
            return RedirectToAction(nameof(Edit), new { id = page.Id });
        }

        [HttpGet("{id:int}/preview")]
        public async Task<IActionResult> Preview(int id)
        {
            var page = await _pageContentService.GetByIdAsync(id);
            if (page is null) return NotFound();
            var html = _pageRenderService.RenderFromJson(page.LayoutJson);
            return View("~/Views/Page/Details.cshtml", new PageRenderViewModel
            {
                Slug = page.Slug,
                Title = $"{page.Title} (Preview)",
                RenderedHtml = html,
                IsPreview = true
            });
        }
    }
}
