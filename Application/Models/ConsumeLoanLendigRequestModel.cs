namespace Application.Models
{
    public class ConsumeLoanLendigRequestModel
    {
        public string NationalCode { get; set; }
        public string Phonenumber { get; set; }
        public long Amount { get; set; }
        public string MerchantCode { get; set; }
    }
    public class ConsumeLoanLendigResponseModel
    {

    }
}
