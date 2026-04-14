namespace eCommerceMvc.Models.Sync
{
    public enum TrendyolStoreEnvironment
    {
        Stage = 0,
        Production = 1
    }

    public class TrendyolStoreConnection
    {
        public int Id { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string SupplierId { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string ApiSecretKey { get; set; } = string.Empty;
        public TrendyolStoreEnvironment Environment { get; set; }
        public string IntegratorName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsVerified { get; set; }
        public DateTime? LastVerifiedAt { get; set; }
        public string? LastErrorMessage { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastProductSyncAtUtc { get; set; }
        public string? LastProductSyncError { get; set; }
    }
}
