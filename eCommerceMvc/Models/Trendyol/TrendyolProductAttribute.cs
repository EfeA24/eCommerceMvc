namespace eCommerceMvc.Models.Trendyol
{
    public class TrendyolProductAttribute
    {
        public int Id { get; set; }
        public long AttributeId { get; set; }
        public string AttributeName { get; set; } = null!;
        public long AttributeValueId { get; set; }
        public string AttributeValue { get; set; } = null!;

        public int TrendyolProductId { get; set; }
        public TrendyolProduct? Product { get; set; }
    }
}
