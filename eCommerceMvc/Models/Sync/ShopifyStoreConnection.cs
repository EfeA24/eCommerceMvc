namespace eCommerceMvc.Models.Sync
{
    public class ShopifyStoreConnection
    {
        public int Id { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string ShopDomain { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string ApiVersion { get; set; } = "2025-10";
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
