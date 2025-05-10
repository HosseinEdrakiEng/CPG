namespace Application.Models
{
    public class BalanceLoanLendigRequestModel
    {
        public string Phonenumber { get; set; }
        public string NationalCode { get; set; }
    }
    public class BalanceLoanLendigResponseModel
    {
        public long Balance { get; set; }
    }
}
