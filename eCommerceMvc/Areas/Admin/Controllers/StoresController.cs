using eCommerceMvc.Context;
using eCommerceMvc.Models.Sync;
using eCommerceMvc.Services.Sync;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/stores")]
    [AutoValidateAntiforgeryToken]
    public class StoresController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly ITrendyolConnectionVerificationService _trendyolVerify;
        private readonly IShopifyConnectionVerificationService _shopifyVerify;

        public StoresController(
            AppDbContext dbContext,
            ITrendyolConnectionVerificationService trendyolVerify,
            IShopifyConnectionVerificationService shopifyVerify)
        {
            _dbContext = dbContext;
            _trendyolVerify = trendyolVerify;
            _shopifyVerify = shopifyVerify;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            ViewBag.TrendyolStores = await _dbContext.TrendyolStoreConnections.AsNoTracking().OrderByDescending(x => x.UpdatedDate).ToListAsync();
            ViewBag.ShopifyStores = await _dbContext.ShopifyStoreConnections.AsNoTracking().OrderByDescending(x => x.UpdatedDate).ToListAsync();
            return View();
        }

        [HttpPost("trendyol/create")]
        public async Task<IActionResult> CreateTrendyol(TrendyolStoreConnection model)
        {
            var now = DateTime.UtcNow;
            model.CreatedDate = now;
            model.UpdatedDate = now;
            var verify = await _trendyolVerify.VerifyAsync(model);
            model.IsVerified = verify.Success;
            model.LastVerifiedAt = verify.Success ? now : null;
            model.LastErrorMessage = verify.Success ? null : verify.ErrorMessage;
            _dbContext.TrendyolStoreConnections.Add(model);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("shopify/create")]
        public async Task<IActionResult> CreateShopify(ShopifyStoreConnection model)
        {
            var now = DateTime.UtcNow;
            model.CreatedDate = now;
            model.UpdatedDate = now;
            var verify = await _shopifyVerify.VerifyAsync(model);
            model.IsVerified = verify.Success;
            model.LastVerifiedAt = verify.Success ? now : null;
            model.LastErrorMessage = verify.Success ? null : verify.ErrorMessage;
            _dbContext.ShopifyStoreConnections.Add(model);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
