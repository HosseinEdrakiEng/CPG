using Application.Models;
using Helper;

namespace Application.Abstraction.IService
{
    public interface ILendingService
    {
        Task<BaseResponse<BalanceLoanLendigResponseModel>> BalanceLoan(BalanceLoanLendigRequestModel model, CancellationToken cancellationToken);
        Task<BaseResponse<ConsumeLoanLendigResponseModel>> ConsumeLoan(ConsumeLoanLendigRequestModel model, CancellationToken cancellationToken);
    }
}
