namespace Application.Common
{
    public enum LoanRequestStatus : byte
    {
        None = 0
    }
    public enum PurchaseStatus : byte
    {
        None = 0,
    }
    public enum LoanStatus : byte
    {
        None = 0,
    }
    public enum InstallmentStatus : byte
    {
        None = 0,
    }
    public enum WalletType : byte
    {

    }
    public enum PurchaseDetailStatus : byte
    {
        None = 0,
        Success = 1,
        Fail = 2
    }
    public enum PurchaseDetailType : byte
    {
        Loan = 1,
        CreditLoan = 2,
        Wallet = 3
    }
}
