namespace eCommerceMvc.Models.Trendyol
{
    public class TrendyolCategory
    {
        public int Id { get; set; }
        public long TrendyolCategoryId { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<TrendyolProduct> Products { get; set; } = new List<TrendyolProduct>();
    }
}
