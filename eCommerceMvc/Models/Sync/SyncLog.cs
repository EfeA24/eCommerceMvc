namespace eCommerceMvc.Models.Sync
{
    public class SyncLog
    {
        public int Id { get; set; }
        public string Channel { get; set; } = string.Empty;
        public string Operation { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
