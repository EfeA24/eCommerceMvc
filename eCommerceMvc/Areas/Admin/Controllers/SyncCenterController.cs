using eCommerceMvc.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/sync-center")]
    public class SyncCenterController : Controller
    {
        private readonly AppDbContext _dbContext;

        public SyncCenterController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var logs = await _dbContext.SyncLogs
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Take(200)
                .ToListAsync();
            return View(logs);
        }
    }
}
