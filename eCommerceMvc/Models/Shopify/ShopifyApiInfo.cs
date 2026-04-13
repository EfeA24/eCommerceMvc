namespace eCommerceMvc.Models.Shopify
{
    public class ShopifyApiInfo
    {
        public string ShopDomain { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public string ApiVersion { get; set; } = "2025-10";
        public bool IsActive { get; set; } = true;
        public bool CanReadProducts { get; set; } = true;
        public bool CanCreateProducts { get; set; } = true;

        public string BaseUrl => $"https://{ShopDomain}/admin/api/{ApiVersion}";
    }
}
