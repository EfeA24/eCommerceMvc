using eCommerceMvc.Models.Sync;

namespace eCommerceMvc.Services.Sync
{
    public class VerificationResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class SyncResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public interface ITrendyolConnectionVerificationService
    {
        Task<VerificationResult> VerifyAsync(TrendyolStoreConnection store, CancellationToken cancellationToken = default);
    }

    public interface IShopifyConnectionVerificationService
    {
        Task<VerificationResult> VerifyAsync(ShopifyStoreConnection store, CancellationToken cancellationToken = default);
    }

    public interface ITrendyolProductSyncService
    {
        Task<SyncResult> SyncStoreAsync(int storeId, CancellationToken cancellationToken = default);
    }

    public interface IShopifyProductSyncService
    {
        Task<SyncResult> SyncStoreAsync(int storeId, CancellationToken cancellationToken = default);
    }
}
