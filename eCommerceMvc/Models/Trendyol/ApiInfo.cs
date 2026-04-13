namespace eCommerceMvc.Models.Trendyol
{
    public enum TrendyolEnvironment
    {
        Stage = 0,
        Production = 1
    }

    public class ApiInfo
    {
        public string SellerId { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public TrendyolEnvironment Environment { get; set; } = TrendyolEnvironment.Production;
        public string IntegratorName { get; set; } = "SelfIntegration";
        public string ProductionBaseUrl { get; set; } = "https://apigw.trendyol.com";
        public string StageBaseUrl { get; set; } = "https://stageapigw.trendyol.com";
        public bool StageIpAuthorized { get; set; }
        public string? StageAuthorizedIp { get; set; }

        public string BaseUrl => Environment == TrendyolEnvironment.Stage ? StageBaseUrl : ProductionBaseUrl;
        public string UserAgent => $"{SellerId} - {IntegratorName}";
    }
}
