namespace eCommerceMvc.Models.Shopify
{
    public class ShopifyProduct
    {
        public int Id { get; set; }
        public long ShopifyProductId { get; set; }
        public string Title { get; set; } = null!;
        public string? BodyHtml { get; set; }
        public string Vendor { get; set; } = null!;
        public string ProductType { get; set; } = null!;
        public string Status { get; set; } = "active";
        public string Handle { get; set; } = null!;
        public string Tags { get; set; } = string.Empty;
        public DateTime? PublishedAtUtc { get; set; }
        public DateTime? LastSyncedAtUtc { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;

        public ICollection<ShopifyVariant> Variants { get; set; } = new List<ShopifyVariant>();
        public ICollection<ShopifyProductImage> Images { get; set; } = new List<ShopifyProductImage>();
        public ICollection<ShopifyProductOption> Options { get; set; } = new List<ShopifyProductOption>();
    }
}
