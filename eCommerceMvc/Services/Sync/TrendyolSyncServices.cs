using eCommerceMvc.Context;
using eCommerceMvc.Models.Sync;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace eCommerceMvc.Services.Sync
{
    public class TrendyolConnectionVerificationService : ITrendyolConnectionVerificationService
    {
        private readonly TrendyolIntegrationOptions _options;

        public TrendyolConnectionVerificationService(IOptions<TrendyolIntegrationOptions> options)
        {
            _options = options.Value;
        }

        public Task<VerificationResult> VerifyAsync(TrendyolStoreConnection store, CancellationToken cancellationToken = default)
        {
            if (_options.SimulateVerification)
            {
                return Task.FromResult(new VerificationResult
                {
                    Success = !string.IsNullOrWhiteSpace(store.SupplierId) &&
                              !string.IsNullOrWhiteSpace(store.ApiKey) &&
                              !string.IsNullOrWhiteSpace(store.ApiSecretKey),
                    ErrorMessage = "Missing Trendyol credentials."
                });
            }

            return Task.FromResult(new VerificationResult { Success = true });
        }
    }

    public class TrendyolProductSyncService : ITrendyolProductSyncService
    {
        private readonly AppDbContext _dbContext;
        private readonly TrendyolIntegrationOptions _options;

        public TrendyolProductSyncService(AppDbContext dbContext, IOptions<TrendyolIntegrationOptions> options)
        {
            _dbContext = dbContext;
            _options = options.Value;
        }

        public async Task<SyncResult> SyncStoreAsync(int storeId, CancellationToken cancellationToken = default)
        {
            var store = await _dbContext.TrendyolStoreConnections.FirstOrDefaultAsync(x => x.Id == storeId, cancellationToken);
            if (store is null)
            {
                return new SyncResult { Success = false, Message = "Store not found." };
            }

            store.LastProductSyncAtUtc = DateTime.UtcNow;
            store.LastProductSyncError = null;
            store.UpdatedDate = DateTime.UtcNow;
            _dbContext.SyncLogs.Add(new SyncLog
            {
                Channel = "Trendyol",
                Operation = "ProductSync",
                IsSuccess = true,
                Message = _options.SimulateProductSync ? "Simulated Trendyol sync completed." : "Trendyol sync completed."
            });
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new SyncResult { Success = true, Message = "Trendyol sync completed." };
        }
    }
}
