namespace Domain.Entites
{
    public class Purchase
    {
        public long Id { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public string TrackingCode { get; set; } = Guid.NewGuid().ToString("N").ToUpper();
        public long MerchantId { get; set; }
        public Merchant Merchant { get; set; }
        public string OrderId { get; set; }
        public long Amount { get; set; }
        public byte Status { get; set; } = 0;
        public string CallbackUrl { get; set; }
        public string? ClientId { get; set; }
        public string Phonenumber { get; set; }
        public string NationalCode { get; set; }

        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }
    }
}
