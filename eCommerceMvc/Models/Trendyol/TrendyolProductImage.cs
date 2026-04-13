namespace eCommerceMvc.Models.Trendyol
{
    public class TrendyolProductImage
    {
        public int Id { get; set; }
        public string Url { get; set; } = null!;
        public int SortOrder { get; set; }

        public int TrendyolProductId { get; set; }
        public TrendyolProduct? Product { get; set; }
    }
}
