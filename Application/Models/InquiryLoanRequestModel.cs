namespace Application.Models
{
    public class BalanceLoanRequestModel
    {
        public string Phonenumber { get; set; }
        public string NationalCode { get; set; }
    }
    public class BalanceLoanResponseModel
    {
        public long Balance { get; set; }
    }
}
