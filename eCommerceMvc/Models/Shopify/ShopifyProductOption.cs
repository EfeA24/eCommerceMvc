namespace eCommerceMvc.Models.Shopify
{
    public class ShopifyProductOption
    {
        public int Id { get; set; }
        public long ShopifyOptionId { get; set; }
        public string Name { get; set; } = null!;
        public int Position { get; set; }
        public string ValuesCsv { get; set; } = string.Empty;

        public int ShopifyProductId { get; set; }
        public ShopifyProduct? Product { get; set; }
    }
}
