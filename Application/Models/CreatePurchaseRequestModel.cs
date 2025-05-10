namespace Application.Models
{
    public class CreatePurchaseRequestModel
    {
        public string OrderId { get; set; }
        public long Amount { get; set; }
        public string CallbackUrl { get; set; }
        public string Phonenumber { get; set; }
        public string NationalCode { get; set; }
    }
    public class CreatePurchaseResponseModel
    {
        public string TrackingCode { get; set; }
    }
}
