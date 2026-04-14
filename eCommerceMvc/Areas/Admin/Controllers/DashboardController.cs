using eCommerceMvc.Areas.Admin.Models;
using eCommerceMvc.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _dbContext;

        public DashboardController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("")]
        [HttpGet("dashboard")]
        public async Task<IActionResult> Index()
        {
            var vm = new AdminDashboardViewModel
            {
                TrendyolStoreCount = await _dbContext.TrendyolStoreConnections.CountAsync(),
                ShopifyStoreCount = await _dbContext.ShopifyStoreConnections.CountAsync(),
                PageCount = await _dbContext.pages.CountAsync(),
                SyncLogCount = await _dbContext.SyncLogs.CountAsync()
            };
            return View(vm);
        }
    }
}
