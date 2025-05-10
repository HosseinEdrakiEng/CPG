using Application.Common;
using Application.Models;
using Helper;

namespace Application.Abstraction.IService
{
    public interface IPurchaseDetailService
    {
        Task<BaseResponse<CreatePurchaseDetailResponseModel>> Create(CreatePurchaseDetailRequestModel request, CancellationToken cancellationToken);
        Task<BaseResponse<ChangeStatusResponseModel>> ChangeStatus(long purchaseDetailId, PurchaseDetailStatus status, CancellationToken cancellationToken);
    }
}
