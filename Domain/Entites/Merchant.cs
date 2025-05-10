namespace Domain.Entites
{
    public class Merchant
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string TerminalId { get; set; }
        public string MerchantCode { get; set; }

        public virtual ICollection<MerchantWallet> MerchantWallets { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
