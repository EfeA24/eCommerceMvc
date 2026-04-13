namespace eCommerceMvc.Models.Trendyol
{
    public class TrendyolBrand
    {
        public int Id { get; set; }
        public long TrendyolBrandId { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<TrendyolProduct> Products { get; set; } = new List<TrendyolProduct>();
    }
}
