using Application.Models;
using Helper;

namespace Application.Abstraction.IService
{
    public interface ILoanService
    {
        Task<BaseResponse<BalanceLoanResponseModel>> Balance(BalanceLoanRequestModel request, CancellationToken cancellationToken);
        Task<BaseResponse<ConsumeLoanResponseModel>> Consume(ConsumeLoanRequestModel request, CancellationToken cancellationToken);
    }
}
