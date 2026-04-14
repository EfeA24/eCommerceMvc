using eCommerceMvc.Context;
using eCommerceMvc.Services.Sync;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/products")]
    [AutoValidateAntiforgeryToken]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly ITrendyolProductSyncService _trendyolProductSyncService;
        private readonly IShopifyProductSyncService _shopifyProductSyncService;

        public ProductsController(
            AppDbContext dbContext,
            ITrendyolProductSyncService trendyolProductSyncService,
            IShopifyProductSyncService shopifyProductSyncService)
        {
            _dbContext = dbContext;
            _trendyolProductSyncService = trendyolProductSyncService;
            _shopifyProductSyncService = shopifyProductSyncService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            ViewBag.TrendyolProducts = await _dbContext.trendyolProducts.AsNoTracking().OrderByDescending(x => x.UpdatedAtUtc).Take(100).ToListAsync();
            ViewBag.ShopifyProducts = await _dbContext.shopifyProducts.AsNoTracking().OrderByDescending(x => x.UpdatedAtUtc).Take(100).ToListAsync();
            ViewBag.TrendyolStores = await _dbContext.TrendyolStoreConnections.AsNoTracking().ToListAsync();
            ViewBag.ShopifyStores = await _dbContext.ShopifyStoreConnections.AsNoTracking().ToListAsync();
            return View();
        }

        [HttpPost("sync/trendyol/{storeId:int}")]
        public async Task<IActionResult> SyncTrendyol(int storeId)
        {
            var result = await _trendyolProductSyncService.SyncStoreAsync(storeId);
            TempData["SyncMessage"] = result.Message;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("sync/shopify/{storeId:int}")]
        public async Task<IActionResult> SyncShopify(int storeId)
        {
            var result = await _shopifyProductSyncService.SyncStoreAsync(storeId);
            TempData["SyncMessage"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
