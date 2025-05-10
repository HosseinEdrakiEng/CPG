using Helper;
using System.Net;

namespace Application.Common
{
    public class CustomError
    {
        public static readonly Error InvalidPurchase = new("01", "Invalid purchase", HttpStatusCode.BadRequest);
        public static readonly Error NotifyOtpFail = new("02", "Notify otp fail", HttpStatusCode.BadRequest);
        public static readonly Error InvalidOtpCode = new("03", "Invalid otp code", HttpStatusCode.BadRequest);
        public static readonly Error InvalidPurchaseAmount = new("04", "Invalid purchase amount", HttpStatusCode.BadRequest);
        public static readonly Error BalanceLoanFail = new("05", "Balance loan fail", HttpStatusCode.BadRequest);
        public static readonly Error PurchaseDetailNotFound = new("06", "Purchase detail not found", HttpStatusCode.BadRequest);
        public static readonly Error InvalidConsumeRequest = new("07", "Invalid consume request", HttpStatusCode.BadRequest);
        public static readonly Error ConsumeLoanFail = new("08", "Consume loan fail", HttpStatusCode.BadRequest);
    }
}
