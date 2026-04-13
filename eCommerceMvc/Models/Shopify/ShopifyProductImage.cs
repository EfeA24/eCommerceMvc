namespace eCommerceMvc.Models.Shopify
{
    public class ShopifyProductImage
    {
        public int Id { get; set; }
        public long ShopifyImageId { get; set; }
        public string Src { get; set; } = null!;
        public int Position { get; set; }
        public string? Alt { get; set; }

        public int ShopifyProductId { get; set; }
        public ShopifyProduct? Product { get; set; }
    }
}
