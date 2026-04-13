namespace eCommerceMvc.Models.Trendyol
{
    public class TrendyolProduct
    {
        public int Id { get; set; }
        public string Barcode { get; set; } = null!;
        public string ProductMainId { get; set; } = null!;
        public string? StockCode { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public decimal ListPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int Quantity { get; set; }
        public int VatRate { get; set; }
        public string CurrencyType { get; set; } = "TRY";
        public bool Approved { get; set; }
        public bool Archived { get; set; }
        public DateTime? LastSyncedAtUtc { get; set; }
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;

        public int TrendyolCategoryId { get; set; }
        public TrendyolCategory? Category { get; set; }

        public int TrendyolBrandId { get; set; }
        public TrendyolBrand? Brand { get; set; }

        public ICollection<TrendyolProductImage> Images { get; set; } = new List<TrendyolProductImage>();
        public ICollection<TrendyolProductAttribute> Attributes { get; set; } = new List<TrendyolProductAttribute>();
    }
}
