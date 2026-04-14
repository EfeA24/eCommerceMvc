using eCommerceMvc.Models.PageContentEntities;
using eCommerceMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/media")]
    public class MediaController : Controller
    {
        private readonly IPageMediaService _pageMediaService;

        public MediaController(IPageMediaService pageMediaService)
        {
            _pageMediaService = pageMediaService;
        }

        [HttpPost("upload-image")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadImage(ImageUploadRequest request)
        {
            try
            {
                var result = await _pageMediaService.UploadImageAsync(request);
                return Json(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
