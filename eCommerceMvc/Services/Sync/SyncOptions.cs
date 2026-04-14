namespace eCommerceMvc.Services.Sync
{
    public class TrendyolIntegrationOptions
    {
        public const string SectionName = "Integrations:Trendyol";
        public bool SimulateVerification { get; set; } = true;
        public bool SimulateProductSync { get; set; } = true;
        public string StageBaseUrl { get; set; } = "https://stageapi.trendyol.com";
        public string ProductionBaseUrl { get; set; } = "https://api.trendyol.com";
    }

    public class ShopifyIntegrationOptions
    {
        public const string SectionName = "Integrations:Shopify";
        public bool SimulateVerification { get; set; } = true;
        public bool SimulateProductSync { get; set; } = true;
        public string ApiVersion { get; set; } = "2025-10";
    }
}
