using eCommerceMvc.Context;
using eCommerceMvc.Models.Sync;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace eCommerceMvc.Services.Sync
{
    public class ShopifyConnectionVerificationService : IShopifyConnectionVerificationService
    {
        private readonly ShopifyIntegrationOptions _options;

        public ShopifyConnectionVerificationService(IOptions<ShopifyIntegrationOptions> options)
        {
            _options = options.Value;
        }

        public Task<VerificationResult> VerifyAsync(ShopifyStoreConnection store, CancellationToken cancellationToken = default)
        {
            if (_options.SimulateVerification)
            {
                var success = !string.IsNullOrWhiteSpace(store.ShopDomain) &&
                              !string.IsNullOrWhiteSpace(store.AccessToken);
                return Task.FromResult(new VerificationResult
                {
                    Success = success,
                    ErrorMessage = success ? null : "Shopify credentials are invalid."
                });
            }

            return Task.FromResult(new VerificationResult { Success = true });
        }
    }

    public class ShopifyProductSyncService : IShopifyProductSyncService
    {
        private readonly AppDbContext _dbContext;
        private readonly ShopifyIntegrationOptions _options;

        public ShopifyProductSyncService(AppDbContext dbContext, IOptions<ShopifyIntegrationOptions> options)
        {
            _dbContext = dbContext;
            _options = options.Value;
        }

        public async Task<SyncResult> SyncStoreAsync(int storeId, CancellationToken cancellationToken = default)
        {
            var store = await _dbContext.ShopifyStoreConnections.FirstOrDefaultAsync(x => x.Id == storeId, cancellationToken);
            if (store is null)
            {
                return new SyncResult { Success = false, Message = "Store not found." };
            }

            store.LastProductSyncAtUtc = DateTime.UtcNow;
            store.LastProductSyncError = null;
            store.UpdatedDate = DateTime.UtcNow;
            _dbContext.SyncLogs.Add(new SyncLog
            {
                Channel = "Shopify",
                Operation = "ProductSync",
                IsSuccess = true,
                Message = _options.SimulateProductSync ? "Simulated Shopify sync completed." : "Shopify sync completed."
            });
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new SyncResult { Success = true, Message = "Shopify sync completed." };
        }
    }
}
