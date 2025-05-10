namespace Application.Models
{
    public class ConsumeLoanRequestModel
    {
        public string TrackingCode { get; set; }
        public long PurchaseDetailId { get; set; }
        public string Phonenumber { get; set; }
        public string NationalCode { get; set; }
    }

    public class ConsumeLoanResponseModel
    {
    }
}
