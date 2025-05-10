using Application.Common;

namespace Application.Models
{
    public class CreatePurchaseDetailRequestModel
    {
        public long Amount { get; set; }
        public string TrackingCode { get; set; }
        public string UserId { get; set; }
        public PurchaseDetailType Type { get; set; }
    }
    public class CreatePurchaseDetailResponseModel
    {
        public long PurchaseDetailId { get; set; }
    }
}
