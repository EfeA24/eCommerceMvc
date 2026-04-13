using eCommerceMvc.Models.PageContentEntities;
using eCommerceMvc.Models.Shopify;
using eCommerceMvc.Models.Trendyol;
using Microsoft.EntityFrameworkCore;

namespace eCommerceMvc.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //base
        public DbSet<ContentBase> contentBases { get; set;  }
        public DbSet<Images> images { get; set; }
        public DbSet<PageContent> pages { get; set; }
        public DbSet<Symbol> symbols { get; set; }

        //shopify
        public DbSet<ShopifyApiInfo> shopifyApiInfos { get; set; }
        public DbSet<ShopifyProduct> shopifyProducts { get; set; }
        public DbSet<ShopifyProductImage> shopifyProductImages { get; set; }
        public DbSet<ShopifyProductOption> shopifyProductOptions {get; set; }
        public DbSet<ShopifyVariant> shopifyVariants { get; set; }

        //trendyol
        public DbSet<TrendyolApiInfo> trendyolApiInfos { get; set; }
        public DbSet<TrendyolBrand> trendyolBrands { get; set; }
        public DbSet<TrendyolCategory> trendyolCategories { get; set; }
        public DbSet<TrendyolProduct> trendyolProducts { get; set; }
        public DbSet<TrendyolProductAttribute> trendyolProductAttributes { get; set; }
        public DbSet<TrendyolProductImage> trendyolProductImages { get; set; }
    }
}
