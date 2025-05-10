namespace Domain.Entites
{
    public class MerchantWallet
    {
        public long MerchantId { get; set; }
        public Merchant Merchant { get; set; }

        public string WalletId { get; set; }
        public byte Type { get; set; }
    }
}
