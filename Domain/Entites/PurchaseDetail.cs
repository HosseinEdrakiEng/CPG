namespace Domain.Entites
{
    public class PurchaseDetail
    {
        public long Id { get; set; }
        public long PurchaseId { get; set; }
        public Purchase Purchase { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public long Amount { get; set; }
        public byte Status { get; set; }
        public byte Type { get; set; }
        public string UserId { get; set; }
    }
}
