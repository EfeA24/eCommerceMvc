namespace eCommerceMvc.Models.Shopify
{
    public class ShopifyVariant
    {
        public int Id { get; set; }
        public long ShopifyVariantId { get; set; }
        public string Title { get; set; } = "Default Title";
        public string? Sku { get; set; }
        public string? Barcode { get; set; }
        public decimal Price { get; set; }
        public decimal? CompareAtPrice { get; set; }
        public int InventoryQuantity { get; set; }
        public bool Taxable { get; set; } = true;
        public string InventoryPolicy { get; set; } = "deny";
        public string? InventoryManagement { get; set; } = "shopify";
        public decimal Weight { get; set; }
        public string WeightUnit { get; set; } = "kg";

        public int ShopifyProductId { get; set; }
        public ShopifyProduct? Product { get; set; }
    }
}
