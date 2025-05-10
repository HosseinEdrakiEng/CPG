using Application.Common;

namespace Application.Models
{
    public class InquiryPurchaseResponseModel
    {
        public long Id { get; set; }
        public string OrderId { get; set; }
        public long Amount { get; set; }
        public string CallbackUrl { get; set; }
        public string Phonenumber { get; set; }
        public string NationalCode { get; set; }
        public PurchaseStatus Status { get; set; }
        public string TrackingCode { get; set; }
        public string MerchantCode { get; set; }
        public List<InquiryPurchaseDetailResponseModel> Details { get; set; } = [];
    }

    public class InquiryPurchaseDetailResponseModel
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public long Amount { get; set; }
        public PurchaseDetailStatus Status { get; set; }
        public PurchaseDetailType Type { get; set; }
    }
}
