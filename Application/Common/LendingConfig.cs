namespace Application.Common
{
    public class LendingConfig
    {
        public string BaseUrl { get; set; }
        public TimeSpan Timeout { get; set; }
        public string GetBalanceUrl { get; set; }
        public string ConsumeLoanUrl { get; set; }
    }
}
